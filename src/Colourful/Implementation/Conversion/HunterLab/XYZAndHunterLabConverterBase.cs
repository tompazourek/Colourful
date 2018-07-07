namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Base class for converters between XYZ and Hunter Lab
    /// </summary>
    public abstract class XYZAndHunterLabConverterBase
    {
        /// <summary>
        /// Computes the Ka parameter
        /// </summary>
        protected static double ComputeKa(XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 175;

            var Ka = 100 * (175 / 198.04) * (whitePoint.X + whitePoint.Y);
            return Ka;
        }

        /// <summary>
        /// Computes the Kb parameter
        /// </summary>
        protected static double ComputeKb(XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 70;

            var Ka = 100 * (70 / 218.11) * (whitePoint.Y + whitePoint.Z);
            return Ka;
        }
    }
}