using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="LinearRGBColor"/> space.
    /// </summary>
    public static class LinearRGBConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="LinearRGBColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint_EqRGBPrimaries<LinearRGBColor>();
            yield return new Convert_EqWhitePoint<LinearRGBColor, XYZColor>((source, _) => new LinearRGBToXYZConversion(source.GetWhitePointRequired(), source.GetRGBPrimariesRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LinearRGBColor>((_, target) => new XYZToLinearRGBConversion(target.GetWhitePointRequired(), target.GetRGBPrimariesRequired()));
            yield return new Intermediate_ToAny_UseSourceWhitePoint<LinearRGBColor, XYZColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<LinearRGBColor, XYZColor>();
        }
    }
}