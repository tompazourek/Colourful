using System;



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
        protected static double[,] GetRGBToXYZMatrix(IRGBWorkingSpace workingSpace)
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

            var S = new[,]
            {
                { Xr, Xg, Xb },
                { Yr, Yg, Yb },
                { Zr, Zg, Zb },
            }.Inverse();

            var W = workingSpace.WhitePoint.Vector;

            var SW = S.MultiplyBy(W);
            var Sr = SW[0];
            var Sg = SW[1];
            var Sb = SW[2];

            double[,] M = 
            {
                { Sr * Xr, Sg * Xg, Sb * Xb },
                { Sr * Yr, Sg * Yg, Sb * Yb },
                { Sr * Zr, Sg * Zg, Sb * Zb },
            };

            return M;
        }

        /// <summary>
        /// Computes XYZ/RGB matrix
        /// </summary>
        protected static double[,] GetXYZToRGBMatrix(IRGBWorkingSpace workingSpace) => GetRGBToXYZMatrix(workingSpace).Inverse();
    }
}