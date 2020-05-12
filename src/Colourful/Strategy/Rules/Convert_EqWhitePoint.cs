using System;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Strategy.Rules
{
    public class Convert_EqWhitePoint<TSource, TTarget> : IConversionRule<TSource, TTarget>
        where TSource : struct
        where TTarget : struct
    {
        private readonly Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> _conversionFactory;

        public Convert_EqWhitePoint(Func<IConversionMetadata, IConversionMetadata, IColorConversion<TSource, TTarget>> conversionFactory)
        {
            _conversionFactory = conversionFactory;
        }

        public virtual int Priority => ConversionRulePriorities.Convert;

        public virtual bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IColorConversion<TSource, TTarget> conversion)
        {
            conversion = null;

            if (!EqualWhitePoints(in sourceNode, in targetNode))
                return false;

            conversion = _conversionFactory(sourceNode, targetNode);
            return true;
        }
    }
}