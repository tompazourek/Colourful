using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LChuvConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<LChuvColor>();
            yield return new Convert_Always<LChuvColor, LuvColor>((source, _) => new LChuvToLuvConversion());
            yield return new Convert_Always<LuvColor, LChuvColor>((_, target) => new LuvToLChuvConversion());
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}