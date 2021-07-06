using static Colourful.Internals.MatrixUtils;

namespace Colourful.Internals
{
    /// <summary>
    /// Utils for conversions between Linear RGB and XYZ.
    /// </summary>
    internal static class LinearRGBConversionUtils
    {
        /// <summary>
        /// Computes the RGB/XYZ matrix.
        /// </summary>
        public static double[,] GetRGBToXYZMatrix(in RGBPrimaries sourcePrimaries, in XYZColor sourceWhitePoint)
        {
            // for more info, see: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

            double xr = sourcePrimaries.R.x,
                xg = sourcePrimaries.G.x,
                xb = sourcePrimaries.B.x,
                yr = sourcePrimaries.R.y,
                yg = sourcePrimaries.G.y,
                yb = sourcePrimaries.B.y;

            var Xr = xr / yr;
            const double Yr = 1;
            var Zr = (1 - xr - yr) / yr;

            var Xg = xg / yg;
            const double Yg = 1;
            var Zg = (1 - xg - yg) / yg;

            var Xb = xb / yb;
            const double Yb = 1;
            var Zb = (1 - xb - yb) / yb;

            var S = Inverse(new[,]
            {
                { Xr, Xg, Xb },
                { Yr, Yg, Yb },
                { Zr, Zg, Zb },
            });

            var W = sourceWhitePoint.Vector;

            var SW = MultiplyBy(in S, in W);
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
        /// Computes the XYZ/RGB matrix.
        /// </summary>
        public static double[,] GetXYZToRGBMatrix(in RGBPrimaries targetPrimaries, in XYZColor targetWhitePoint)
            => Inverse(GetRGBToXYZMatrix(in targetPrimaries, in targetWhitePoint));
    }
}
