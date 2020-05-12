using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LabConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<LabColor>();
            yield return new Convert_EqWhitePoint<LabColor, XYZColor>((source, _) => new LabToXYZConversion(source.GetWhitePointRequired()));
            yield return new Convert_EqWhitePoint<XYZColor, LabColor>((_, target) => new XYZToLabConversion(target.GetWhitePointRequired()));
            yield return new Intermediate_UseSourceWhitePoint<,,>();
            yield return new Intermediate_UseTargetWhitePoint<,,>();
        }
    }
}