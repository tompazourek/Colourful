using System;
using static Colourful.Strategy.Rules.ConversionRuleUtils;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// If the white points and RGB primaries are equal, replaces the two nodes with the target node, and outputs a conversion operation.
    /// </summary>
    /// <typeparam name="TSource">Source color type</typeparam>
    /// <typeparam name="TTarget">Target color type</typeparam>
    public class Convert_EqWhitePoint_EqRGBPrimaries<TSource, TTarget> : IConversionRule
        where TSource : struct
        where TTarget : struct
    {
        private readonly Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> _conversionFactory;

        /// <param name="conversionFactory">Creates conversion between the color spaces using the additional data from the nodes.</param>
        public Convert_EqWhitePoint_EqRGBPrimaries(Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> conversionFactory)
        {
            _conversionFactory = conversionFactory;
        }

        /// <inheritdoc />
        public int Priority => ConversionRulePriorities.Convert;

        /// <inheritdoc />
        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion)
        {
            replacementNodes = new IConversionMetadata[] { };
            conversion = null;

            if (!HaveColorTypes<TSource, TTarget>(in sourceNode, in targetNode))
                return false;

            if (!EqualWhitePoints(in sourceNode, in targetNode))
                return false;

            if (!EqualRGBPrimaries(in sourceNode, in targetNode))
                return false;

            replacementNodes = new[] { targetNode };
            conversion = _conversionFactory(sourceNode, targetNode);
            return true;
        }
    }
}