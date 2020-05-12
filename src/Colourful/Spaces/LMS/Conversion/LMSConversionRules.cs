using Colourful.Strategy.Rules;
using System.Collections.Generic;
using Colourful.Adaptation;
using Colourful.Strategy;

namespace Colourful.Conversion
{
    public static class LMSConversionRules
    {
        public static IEnumerable<IConversionRule<TSource, TTarget>> GetRules<TSource, TTarget>(ConversionFactory conversionFactory, double[,] transformationMatrix)
            where TSource : struct
            where TTarget : struct
        {
            yield return new Bypass_EqWhitePoint<LMSColor>();
            yield return new Convert_NotEqWhitePoint<LMSColor, LMSColor>((source, target) =>
            {
                var sourceWhitePointXYZ = source.GetWhitePointRequired();
                var targetWhitePointXYZ = target.GetWhitePointRequired();

                var whitePointConversion = new XYZToLMSConversion(transformationMatrix);

                var sourceWhitePointLMS = whitePointConversion.Convert(in sourceWhitePointXYZ);
                var targetWhitePointLMS = whitePointConversion.Convert(in targetWhitePointXYZ);
                
                return new VonKriesChromaticAdaptation(in sourceWhitePointLMS, in targetWhitePointLMS);
            });
            yield return new Convert_EqWhitePoint<LMSColor, XYZColor>((source, _) => new LMSToXYZConversion(in transformationMatrix));
            yield return new Convert_EqWhitePoint<XYZColor, LMSColor>((_, target) => new XYZToLMSConversion(in transformationMatrix));
            
            yield return new Intermediate_EqWhitePoint_UseSourceWhitePoint<LMSColor, XYZColor>();
            yield return new Intermediate_EqWhitePoint_UseTargetWhitePoint<LMSColor, XYZColor>();

            yield return new Intermediate_NotEqWhitePoint_UseTargetWhitePoint<LMSColor, LMSColor>();
            yield return new Intermediate_FromAny_NotEqWhitePoint_UseSourceWhitePoint<LMSColor, LMSColor>();
        }
    }
}