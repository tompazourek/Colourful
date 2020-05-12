using System;

namespace Colourful.Strategy.Rules
{
    public class Convert_Always<TSource, TTarget> : IConversionRule<TSource, TTarget>
        where TSource : struct
        where TTarget : struct
    {
        private readonly Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> _conversionFactory;

        public Convert_Always(Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> conversionFactory)
        {
            _conversionFactory = conversionFactory;
        }

        /// <inheritdoc />
        public int Priority => ConversionRulePriorities.Convert;

        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IColorConversion<TSource, TTarget> conversion)
        {
            conversion = _conversionFactory(sourceNode, targetNode);
            return true;
        }
    }
}