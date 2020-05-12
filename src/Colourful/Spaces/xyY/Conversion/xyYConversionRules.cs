using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="xyYColor"/> space.
    /// </summary>
    public static class xyYConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="xyYColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<xyYColor>();
            yield return new Convert_Always<xyYColor, XYZColor>((source, _) => new xyYToXYZConversion());
            yield return new Convert_Always<XYZColor, xyYColor>((_, target) => new XYZToxyYConversion());
            yield return new Intermediate_ToAny_UseSourceWhitePoint<xyYColor, XYZColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<xyYColor, XYZColor>();
        }
    }
}