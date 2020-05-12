using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LuvConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<LuvColor>();
            yield return new Convert_EqWhitePoint<LuvColor, XYZColor>((source, _) => new LuvToXYZConversion(source.GetWhitePointRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LuvColor>((_, target) => new XYZToLuvConversion(target.GetWhitePointRequired()));
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}