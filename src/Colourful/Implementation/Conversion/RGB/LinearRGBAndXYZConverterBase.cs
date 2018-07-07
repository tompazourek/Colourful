using System;
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Base class for conversions between <see cref="RGBColor" /> and <see cref="XYZColor" />.
    /// </summary>
    public abstract class LinearRGBAndXYZConverterBase
    {
        /// <summary>
        /// Computes RGB/XYZ matrix
        /// </summary>
        protected static Matrix GetRGBToXYZMatrix(IRGBWorkingSpace workingSpace)
        {
            if (workingSpace == null) throw new ArgumentNullException(nameof(workingSpace));

            // for more info, see: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

            var chromaticity = workingSpace.ChromaticityCoordinates;
            double xr = chromaticity.R.x,
                xg = chromaticity.G.x,
                xb = chromaticity.B.x,
                yr = chromaticity.R.y,
                yg = chromaticity.G.y,
                yb = chromaticity.B.y;

            var Xr = xr / yr;
            const double Yr = 1;
            var Zr = (1 - xr - yr) / yr;

            var Xg = xg / yg;
            const double Yg = 1;
            var Zg = (1 - xg - yg) / yg;

            var Xb = xb / yb;
            const double Yb = 1;
            var Zb = (1 - xb - yb) / yb;

            var S = new Vector[]
            {
                new[] { Xr, Xg, Xb },
                new[] { Yr, Yg, Yb },
                new[] { Zr, Zg, Zb },
            }.Inverse();

            var W = workingSpace.WhitePoint.Vector;

            var SW = S.MultiplyBy(W);
            var Sr = SW[0];
            var Sg = SW[1];
            var Sb = SW[2];

            Matrix M = new Vector[]
            {
                new[] { Sr * Xr, Sg * Xg, Sb * Xb },
                new[] { Sr * Yr, Sg * Yg, Sb * Yb },
                new[] { Sr * Zr, Sg * Zg, Sb * Zb },
            };

            return M;
        }

        /// <summary>
        /// Computes XYZ/RGB matrix
        /// </summary>
        protected static Matrix GetXYZToRGBMatrix(IRGBWorkingSpace workingSpace) => GetRGBToXYZMatrix(workingSpace).Inverse();
    }
}