using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from XYZ to L*a*b*
    /// </summary>
    public class XYZToLabConverter : IColorConverter<XYZColor, LabColor>
    {
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
            const double epsilon = 216d / 24389d;
            const double kappa = 24389d / 27d;

            double fc = cr > epsilon ? Math.Pow(cr, 1 / 3d) : (kappa * cr + 16) / 116d;
            return fc;
        }
    }
}
