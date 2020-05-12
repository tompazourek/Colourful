using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="LuvColor"/> space.
    /// </summary>
    public static class LuvConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="LuvColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<LuvColor>();
            yield return new Convert_EqWhitePoint<LuvColor, XYZColor>((source, _) => new LuvToXYZConversion(source.GetWhitePointRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LuvColor>((_, target) => new XYZToLuvConversion(target.GetWhitePointRequired()));
            yield return new Intermediate_ToAny_UseSourceWhitePoint<LuvColor, XYZColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<LuvColor, XYZColor>();
        }
    }
}