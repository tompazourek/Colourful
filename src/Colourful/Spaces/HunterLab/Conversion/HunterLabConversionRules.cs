using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="HunterLabColor"/> space.
    /// </summary>
    public static class HunterLabConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="HunterLabColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<HunterLabColor>();
            yield return new Convert_EqWhitePoint<HunterLabColor, XYZColor>((source, _) => new HunterLabToXYZConversion(source.GetWhitePointRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, HunterLabColor>((_, target) => new XYZToHunterLabConversion(target.GetWhitePointRequired()));
            yield return new Intermediate_ToAny_UseSourceWhitePoint<HunterLabColor, XYZColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<HunterLabColor, XYZColor>();
        }
    }
}