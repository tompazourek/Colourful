using System;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// Replaces the two nodes with the target node, and outputs a conversion operation.
    /// </summary>
    /// <typeparam name="TSource">Source color type</typeparam>
    /// <typeparam name="TTarget">Target color type</typeparam>
    public class Convert_Always<TSource, TTarget> : IConversionRule
        where TSource : struct
        where TTarget : struct
    {
        private readonly Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> _conversionFactory;

        /// <param name="conversionFactory">Creates conversion between the color spaces using the additional data from the nodes.</param>
        public Convert_Always(Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> conversionFactory)
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

            if (!ConversionRuleUtils.HaveColorTypes<TSource, TTarget>(in sourceNode, in targetNode))
                return false;

            replacementNodes = new[] { targetNode };
            conversion = _conversionFactory(sourceNode, targetNode);
            return true;
        }
    }
}