using static System.Math;

namespace Colourful.Internals
{
    /// <summary>
    /// Math helper functions.
    /// </summary>
    internal static class MathUtils
    {
        /// <summary>
        /// Compute x^2.
        /// </summary>
        /// <param name="x">Base.</param>
        /// <returns>Result of the exponentiation.</returns>
        public static double Pow2(in double x) => x * x;

        /// <summary>
        /// Compute x^3.
        /// </summary>
        /// <param name="x">Base.</param>
        /// <returns>Result of the exponentiation.</returns>
        public static double Pow3(in double x) => x * x * x;

        /// <summary>
        /// Compute x^4.
        /// </summary>
        /// <param name="x">Base.</param>
        /// <returns>Result of the exponentiation.</returns>
        public static double Pow4(in double x) => x * x * (x * x);

        /// <summary>
        /// Compute x^7.
        /// </summary>
        /// <param name="x">Base.</param>
        /// <returns>Result of the exponentiation.</returns>
        public static double Pow7(in double x) => x * x * x * (x * x * x) * x;

        /// <summary>
        /// Compute sine of angle in degrees.
        /// </summary>
        /// <param name="x">Given angle.</param>
        public static double SinDeg(in double x)
        {
            var x_rad = DegreeToRadian(in x);
            var y = Sin(x_rad);
            return y;
        }

        /// <summary>
        /// Compute cosine of angle in degrees.
        /// </summary>
        /// <param name="x">Given angle.</param>
        public static double CosDeg(in double x)
        {
            var x_rad = DegreeToRadian(in x);
            var y = Cos(x_rad);
            return y;
        }

        private const double TwoPI = 2 * PI;

        public static double RadianToDegree(in double rad)
        {
            var deg = 360 * (rad / TwoPI);
            return deg;
        }

        public static double DegreeToRadian(in double deg)
        {
            var rad = TwoPI * (deg / 360d);
            return rad;
        }

        public static double NormalizeDegree(in double deg)
        {
            var d = deg % 360d;
            return d >= 0 ? d : d + 360d;
        }
    }
}
