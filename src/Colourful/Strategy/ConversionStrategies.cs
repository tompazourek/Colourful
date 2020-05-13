using System.Collections.Generic;
using Colourful.Conversion;

namespace Colourful.Strategy
{
    public static class ConversionStrategies
    {
        /// <summary>
        /// Returns list of conversion strategies that are built-in to the library.
        /// </summary>
        /// <param name="lmsTransformationMatrix">Optionally pick LMS transformation matrix (<see cref="LMSTransformationMatrix" />) used for LMS-XYZ conversion and chromatic adaptation for color spaces with different white points. If empty, <see cref="LMSTransformationMatrix.Bradford" /> will be used.</param>
        /// <returns></returns>
        public static IEnumerable<IConversionStrategy> GetDefault
        (
            double[,] lmsTransformationMatrix = null
        )
        {
            yield return new HunterLabConversionStrategy();
            yield return new LabConversionStrategy();
            yield return new LChabConversionStrategy();
            yield return new LChuvConversionStrategy();
            yield return new LinearRGBConversionStrategy();
            yield return new LMSConversionStrategy(lmsTransformationMatrix ?? LMSTransformationMatrix.Bradford);
            yield return new LuvConversionStrategy();
            yield return new RGBConversionStrategy();
            yield return new xyYConversionStrategy();
            yield return new XYZConversionStrategy();
        }
    }
}