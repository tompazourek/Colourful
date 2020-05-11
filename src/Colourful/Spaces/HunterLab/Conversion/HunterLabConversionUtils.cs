namespace Colourful.Conversion
{
    /// <summary>
    /// Utils for conversions between XYZ and Hunter Lab.
    /// </summary>
    internal static class HunterLabConversionUtils
    {
        /// <summary>
        /// Computes the Ka parameter.
        /// </summary>
        public static double ComputeKa(in XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 175;

            var Ka = 100 * (175 / 198.04) * (whitePoint.X + whitePoint.Y);
            return Ka;
        }

        /// <summary>
        /// Computes the Kb parameter.
        /// </summary>
        public static double ComputeKb(in XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 70;

            var Ka = 100 * (70 / 218.11) * (whitePoint.Y + whitePoint.Z);
            return Ka;
        }
    }
}