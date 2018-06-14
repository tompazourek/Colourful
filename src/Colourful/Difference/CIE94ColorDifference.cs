using System;
using Colourful.Implementation;

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 1994 formula
    /// </summary>
    /// <remarks>
    /// Implementation notes:
    /// http://www.brucelindbloom.com/Eqn_DeltaE_CIE94.html
    /// </remarks>
    public sealed class CIE94ColorDifference : IColorDifference<LabColor>
    {
        private const double KH = 1;
        private const double KC = 1;

        private readonly double K1;
        private readonly double K2;
        private readonly double KL;

        /// <summary>
        /// Construct using weighting factors for <see cref="CIE94ColorDifferenceApplication.GraphicArts" />.
        /// </summary>
        public CIE94ColorDifference() : this(CIE94ColorDifferenceApplication.GraphicArts)
        {
        }

        /// <summary>
        /// Construct using weighting factors for given application of color difference
        /// </summary>
        /// <param name="application">A <see cref="CIE94ColorDifferenceApplication" /> value specifying the application area. Different weighting factors are used in the computation depending on the application.</param>
        public CIE94ColorDifference(CIE94ColorDifferenceApplication application)
        {
            switch (application)
            {
                case CIE94ColorDifferenceApplication.GraphicArts:
                    KL = 1;
                    K1 = 0.045;
                    K2 = 0.015;
                    break;
                case CIE94ColorDifferenceApplication.Textiles:
                    KL = 2;
                    K1 = 0.048;
                    K2 = 0.014;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(application));
            }
        }

        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <returns>Delta-E (1994) color difference</returns>
        public double ComputeDifference(in LabColor x, in LabColor y)
        {
            if (x.WhitePoint != y.WhitePoint)
                throw new ArgumentException("Colors must have same white point to be compared.");

            var da = x.a - y.a;
            var db = x.b - y.b;
            var dL = x.L - y.L;
            var C1 = Math.Sqrt(x.a * x.a + x.b * x.b);
            var C2 = Math.Sqrt(y.a * y.a + y.b * y.b);
            var dC = C1 - C2;
            var dH_sq = da * da + db * db - dC * dC; // dH ^ 2
            const double SL = 1;
            var SC = 1 + K1 * C1;
            var SH = 1 + K2 * C1;
            var dE94 = Math.Sqrt(
                MathUtils.Pow2(dL / (KL * SL)) +
                MathUtils.Pow2(dC / (KC * SC)) +
                dH_sq / MathUtils.Pow2(KH * SH)
            );
            return dE94;
        }
    }

    /// <summary>
    /// Application area for CIE Delta-E 1994
    /// </summary>
    public enum CIE94ColorDifferenceApplication
    {
        /// <summary>
        /// Graphic arts
        /// </summary>
        GraphicArts,

        /// <summary>
        /// Textiles
        /// </summary>
        Textiles
    }
}