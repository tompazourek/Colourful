using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Adaptation;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    /// <summary>
    /// Rules for the <see cref="XYZColor"/> space.
    /// </summary>
    public static class XYZConversionRules
    {
        /// <summary>
        /// Rules for the <see cref="XYZColor"/> space.
        /// </summary>
        public static IEnumerable<IConversionRule> GetRules()
        {
            yield return new Return_EqSpace_EqWhitePoint<XYZColor>();
            yield return new Intermediate_EqSpace_NotEqWhitePoint_UseSourceWhitePoint<XYZColor, LMSColor>();
        }
    }
}