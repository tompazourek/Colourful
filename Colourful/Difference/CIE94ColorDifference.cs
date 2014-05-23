using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 1994 formula
    /// </summary>
    /// <remarks>
    /// Implementation notes:
    /// http://www.brucelindbloom.com/Eqn_DeltaE_CIE94.html
    /// </remarks>
    public class CIE94ColorDifference : IColorDifference<LabColor>
    {
        private const double KH = 1;
        private const double KC = 1;

        private readonly double K1;
        private readonly double K2;
        private readonly double KL;

        /// <summary>
        /// Construct using weighing factors for <see cref="CIE94ColorDifferenceApplication.GraphicArts"/>.
        /// </summary>
        public CIE94ColorDifference() : this(CIE94ColorDifferenceApplication.GraphicArts)
        {
        }

        /// <summary>
        /// Construct using weighing factors for given application of color difference
        /// </summary>
        /// <param name="application"></param>
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
                    throw new ArgumentOutOfRangeException("application");
            }
        }

        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <returns>Delta-E (1994) color difference</returns>
        public double ComputeDifference(LabColor x, LabColor y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            if (x.WhitePoint != y.WhitePoint)
                throw new ArgumentException("Colors must have same white point to be compared.");

            double da = x.a - y.a;
            double db = x.b - y.b;
            double dL = x.L - y.L;
            double C1 = Math.Sqrt(x.a * x.a + x.b * x.b);
            double C2 = Math.Sqrt(y.a * y.a + y.b * y.b);
            double dC = C1 - C2;
            double dH_sq = da * da + db * db - dC * dC; // dH ^ 2
            const double SL = 1;
            double SC = 1 + K1 * C1;
            double SH = 1 + K2 * C1;
            double dE94 = Math.Sqrt(
                Math.Pow(dL / (KL * SL), 2) +
                    Math.Pow(dC / (KC * SC), 2) +
                    dH_sq / Math.Pow(KH * SH, 2)
                );
            return dE94;
        }
    }

    public enum CIE94ColorDifferenceApplication
    {
        GraphicArts,
        Textiles
    };
}