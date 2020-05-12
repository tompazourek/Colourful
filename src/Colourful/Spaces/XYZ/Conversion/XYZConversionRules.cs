using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Adaptation;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class XYZConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<XYZColor>();
            yield return new Intermediate_NotEqWhitePoint_UseSourceWhitePoint<XYZColor, LMSColor>();
        }
    }
}