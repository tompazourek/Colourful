using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="LChabColor"/> space.
    /// </summary>
    public static class LChabConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="LChabColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<LChabColor>();
            yield return new Convert_Always<LChabColor, LabColor>((source, _) => new LChabToLabConversion());
            yield return new Convert_Always<LabColor, LChabColor>((_, target) => new LabToLChabConversion());
            yield return new Intermediate_ToAny_UseSourceWhitePoint<LChabColor, LabColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<LChabColor, LabColor>();
        }
    }
}