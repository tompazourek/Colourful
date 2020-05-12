using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LChabConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<LChabColor>();
            yield return new Convert_Always<LChabColor, LabColor>((source, _) => new LChabToLabConversion());
            yield return new Convert_Always<LabColor, LChabColor>((_, target) => new LabToLChabConversion());
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}