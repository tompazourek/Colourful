using System;
using Colourful.ChromaticAdaptation;
using Colourful.Colors;
using Colourful.Implementation.RGB;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    public abstract class RGBAndXYZConverterBase
    {
        private static readonly BradfordChromaticAdaptation BradfordChromaticAdaptation = new BradfordChromaticAdaptation();

        /// <summary>
        /// <see cref="IChromaticAdaptation"/>
        /// </summary>
        public IChromaticAdaptation ChromaticAdaptation { get; protected set; }

        /// <summary>
        /// Bradford chromatic adaptation.
        /// Used when chromatic adaptation for reference white adjustation is not specified explicitly.
        /// </summary>
        public static IChromaticAdaptation DefaultChromaticAdaptation
        {
            get { return BradfordChromaticAdaptation; }
        }

        protected static Matrix<double> GetRGBToXYZMatrix(IRGBWorkingSpace workingSpace)
        {
            if (workingSpace == null) throw new ArgumentNullException("workingSpace");

            // for more info, see: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

            RGBPrimariesChromaticityCoordinates chromaticity = workingSpace.ChromaticityCoordinates;
            double xr = chromaticity.R.x, xg = chromaticity.G.x, xb = chromaticity.B.x,
                   yr = chromaticity.R.y, yg = chromaticity.G.y, yb = chromaticity.B.y;

            double Sr, Sg, Sb;

            double Xr = xr / yr;
            const double Yr = 1;
            double Zr = (1 - xr - yr) / yr;

            double Xg = xg / yg;
            const double Yg = 1;
            double Zg = (1 - xg - yg) / yg;

            double Xb = xb / yb;
            const double Yb = 1;
            double Zb = (1 - xb - yb) / yb;

            {
                Matrix<double> S = DenseMatrix.OfRows(3, 3, new[]
                    {
                        new[] { Xr, Xg, Xb },
                        new[] { Yr, Yg, Yb },
                        new[] { Zr, Zg, Zb },
                    }).Inverse();

                Vector<double> W = workingSpace.ReferenceWhite.Vector;

                (S * W).AssignVariables(out Sr, out Sg, out Sb);
            }

            DenseMatrix M = DenseMatrix.OfRows(3, 3, new[]
                {
                    new[] { Sr * Xr, Sg * Xg, Sb * Xb },
                    new[] { Sr * Yr, Sg * Yg, Sb * Yb },
                    new[] { Sr * Zr, Sg * Zg, Sb * Zb },
                });

            return M;
        }

        protected static Matrix<double> GetXYZToRGBMatrix(IRGBWorkingSpace workingSpace)
        {
            return GetRGBToXYZMatrix(workingSpace).Inverse();
        }
    }
}