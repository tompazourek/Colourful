using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class RGBConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint_EqRGBPrimaries<RGBColor>();
            yield return new Convert_EqWhitePoint_EqRGBPrimaries<RGBColor, LinearRGBColor>((source, _) => new RGBToLinearRGBConversion(source.GetCompandingRequired()));
            yield return new Convert_EqWhitePoint_EqRGBPrimaries<LinearRGBColor, RGBColor>((_, target) => new LinearRGBToRGBConversion(target.GetCompandingRequired()));
            yield return new Intermediate_UseSourceWhitePoint_UseSourceRGBPrimaries<RGBColor, LinearRGBColor>();
            yield return new Intermediate_UseTargetWhitePoint_UseTargetRGBPrimaries<RGBColor, LinearRGBColor>();
        }
    }
}