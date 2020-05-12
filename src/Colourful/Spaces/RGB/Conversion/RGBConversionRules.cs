using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="RGBColor"/> space.
    /// </summary>
    public static class RGBConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="RGBColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint_EqRGBPrimaries<RGBColor>();
            yield return new Convert_EqWhitePoint_EqRGBPrimaries<RGBColor, LinearRGBColor>((source, _) => new RGBToLinearRGBConversion(source.GetCompandingRequired()));
            yield return new Convert_EqWhitePoint_EqRGBPrimaries<LinearRGBColor, RGBColor>((_, target) => new LinearRGBToRGBConversion(target.GetCompandingRequired()));
            yield return new Intermediate_ToAny_UseSourceWhitePoint_UseSourceRGBPrimaries<RGBColor, LinearRGBColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint_UseTargetRGBPrimaries<RGBColor, LinearRGBColor>();
        }
    }
}