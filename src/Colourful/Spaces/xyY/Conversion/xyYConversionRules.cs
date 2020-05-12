using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class xyYConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<xyYColor>();
            yield return new Convert_Always<xyYColor, XYZColor>((source, _) => new xyYToXYZConversion());
            yield return new Convert_Always<XYZColor, xyYColor>((_, target) => new XYZToxyYConversion());
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}