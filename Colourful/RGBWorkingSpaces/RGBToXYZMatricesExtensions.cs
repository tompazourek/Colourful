using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.RGBWorkingSpaces
{
    internal static class RGBToXYZMatricesExtensions
    {
        public static Matrix<double> GetRGBToXYZMatrix(this IRGBWorkingSpace workingSpace)
        {
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

            RGBSystemChromacityCoordinates chromacity = workingSpace.ChromacityCoordinates;
            double xr = chromacity.xr, xg = chromacity.xg, xb = chromacity.xb,
                   yr = chromacity.yr, yg = chromacity.yg, yb = chromacity.yb;

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

        public static Matrix<double> GetXYZToRGBMatrix(this IRGBWorkingSpace workingSpace)
        {
            return GetRGBToXYZMatrix(workingSpace).Inverse();
        }
    }
}