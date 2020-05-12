using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class HunterLabConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            if (typeof(TSource) == typeof(HunterLabColor) && typeof(TTarget) == typeof(HunterLabColor))
                yield return new Bypass_EqWhitePoint<HunterLabColor>() as IConversionRule<TSource, TTarget>;

            if (typeof(TSource) == typeof(HunterLabColor) && typeof(TTarget) == typeof(XYZColor))
                yield return new Convert_EqWhitePoint<HunterLabColor, XYZColor>((source, _) => new HunterLabToXYZConversion(source.GetWhitePointRequired())) as IConversionRule<TSource, TTarget>;

            if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(HunterLabColor))
                yield return new Convert_EqWhitePoint<XYZColor, HunterLabColor>((_, target) => new XYZToHunterLabConversion(target.GetWhitePointRequired())) as IConversionRule<TSource, TTarget>;

            if (typeof(TSource) == typeof(HunterLabColor))
                yield return new Intermediate_UseSourceWhitePoint<HunterLabColor, XYZColor, TTarget>(conversionFactory) as IConversionRule<TSource, TTarget>;

            if (typeof(TTarget) == typeof(HunterLabColor))
                yield return new Intermediate_UseTargetWhitePoint<TSource, XYZColor, HunterLabColor>(conversionFactory) as IConversionRule<TSource, TTarget>;
        }
    }
}