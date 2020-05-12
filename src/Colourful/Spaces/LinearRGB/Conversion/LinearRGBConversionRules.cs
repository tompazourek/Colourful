using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LinearRGBConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint_EqRGBPrimaries<LinearRGBColor>();
            yield return new Convert_EqWhitePoint<LinearRGBColor, XYZColor>((source, _) => new LinearRGBToXYZConversion(source.GetWhitePointRequired(), source.GetRGBPrimariesRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LinearRGBColor>((_, target) => new XYZToLinearRGBConversion(target.GetWhitePointRequired(), target.GetRGBPrimariesRequired()));
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}