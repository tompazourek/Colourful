using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="LabColor"/> space.
    /// </summary>
    public static class LabConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="LabColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<LabColor>();
            yield return new Convert_EqWhitePoint<LabColor, XYZColor>((source, _) => new LabToXYZConversion(source.GetWhitePointRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LabColor>((_, target) => new XYZToLabConversion(target.GetWhitePointRequired()));
            yield return new Intermediate_ToAny_UseSourceWhitePoint<LabColor, XYZColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<LabColor, XYZColor>();
        }
    }
}