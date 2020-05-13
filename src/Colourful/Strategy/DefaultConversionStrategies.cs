using Colourful.Conversion;

namespace Colourful.Strategy
{
    public static class DefaultConversionStrategies
    {
        /// <summary>
        /// Registers the conversion strategies to convert among built-in color spaces.
        /// </summary>
        /// <param name="converterFactory"></param>
        /// <param name="lmsTransformationMatrix">Optionally pick LMS transformation matrix (<see cref="LMSTransformationMatrix" />), if empty, <see cref="LMSTransformationMatrix.Bradford" /> will be used.</param>
        public static void RegisterDefaultStrategies(this IConverterFactory converterFactory, double[,] lmsTransformationMatrix = null)
        {
            converterFactory.RegisterStrategy(new HunterLabConversionStrategy());
            converterFactory.RegisterStrategy(new LabConversionStrategy());
            converterFactory.RegisterStrategy(new LChabConversionStrategy());
            converterFactory.RegisterStrategy(new LChuvConversionStrategy());
            converterFactory.RegisterStrategy(new LinearRGBConversionStrategy());
            converterFactory.RegisterStrategy(new LMSConversionStrategy(lmsTransformationMatrix ?? LMSTransformationMatrix.Bradford));
            converterFactory.RegisterStrategy(new LuvConversionStrategy());
            converterFactory.RegisterStrategy(new RGBConversionStrategy());
            converterFactory.RegisterStrategy(new xyYConversionStrategy());
            converterFactory.RegisterStrategy(new XYZConversionStrategy());
        }
    }
}