using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="LChuvColor"/> space.
    /// </summary>
    public static class LChuvConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="LChuvColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<LChuvColor>();
            yield return new Convert_Always<LChuvColor, LuvColor>((source, _) => new LChuvToLuvConversion());
            yield return new Convert_Always<LuvColor, LChuvColor>((_, target) => new LuvToLChuvConversion());
            yield return new Intermediate_ToAny_UseSourceWhitePoint<LChuvColor, LuvColor>();
            yield return new Intermediate_FromAny_UseTargetWhitePoint<LChuvColor, LuvColor>();
        }
    }
}