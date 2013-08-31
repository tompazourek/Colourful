using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from XYZ to L*a*b* and backwards
    /// </summary>
    public class XYZAndLabConverter : IColorConverter<XYZColor, LabColor>, IColorConverter<LabColor, XYZColor>
    {
        private const double Epsilon = 216d / 24389d;
        private const double Kappa = 24389d / 27d;

        #region XYZ to Lab

        public LabColor Convert(XYZColor input)
        {
            // Conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Lab.html
            double Xr = input.ReferenceWhite.X, Yr = input.ReferenceWhite.Y, Zr = input.ReferenceWhite.Z;

            double xr = input.X / Xr, yr = input.Y / Yr, zr = input.Z / Zr;

            double fx = f(xr);
            double fy = f(yr);
            double fz = f(zr);

            double L = 116 * fy - 16;
            double a = 500 * (fx - fy);
            double b = 200 * (fy - fz);

            var output = new LabColor(L, a, b);
            return output;
        }

        private double f(double cr)
        {
            double fc = cr > Epsilon ? Math.Pow(cr, 1 / 3d) : (Kappa * cr + 16) / 116d;
            return fc;
        }

        #endregion

        #region Lab to XYZ

        /// <summary>
        /// Target reference white is <see cref="XYZColor.DefaultReferenceWhite"/>
        /// <seealso cref="Convert(LabColor, XYZColorBase)"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public XYZColor Convert(LabColor input)
        {
            var result = Convert(input, XYZColor.DefaultReferenceWhite);
            return result;
        }

        public XYZColor Convert(LabColor input, XYZColorBase referenceWhite)
        {
            // Conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_Lab_to_XYZ.html
            double L = input.L, a = input.a, b = input.b;
            double fy = (L + 16) / 116d;
            double fx = a / 500d + fy;
            double fz = fy - b / 200d;

            double fx3 = Math.Pow(fx, 3);
            double fz3 = Math.Pow(fz, 3);

            double xr = fx3 > Epsilon ? fx3 : (116 * fx - 16) / Kappa;
            double yr = L > Kappa * Epsilon ? Math.Pow((L + 16) / 116d, 3) : L / Kappa;
            double zr = fz3 > Epsilon ? fz3 : (116 * fz - 16) / Kappa;

            double Xr = referenceWhite.X, Yr = referenceWhite.Y, Zr = referenceWhite.Z;

            // avoids XYZ coordinates out of XYZ ref. white range
            xr = xr.CropRange(0, 1);
            yr = yr.CropRange(0, 1);
            zr = zr.CropRange(0, 1);

            double X = xr * Xr;
            double Y = yr * Yr;
            double Z = zr * Zr;

            var result = new XYZColor(X, Y, Z, referenceWhite);
            return result;
        }

        #endregion
    }
}
