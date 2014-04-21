using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor"/> to <see cref="LabColor"/>.
    /// </summary>
    public class XYZToLabConverter : XYZAndLabConverterBase, IColorConversion<XYZColor, LabColor>
    {
        public XYZToLabConverter()
            : this(LabColor.DefaultWhitePoint)
        {
        }

        public XYZToLabConverter(XYZColor labWhitePoint)
        {
            LabWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LabColor.DefaultWhitePoint"/> is used.
        /// </summary>
        public XYZColor LabWhitePoint { get; private set; }

        public LabColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Lab.html
            double Xr = LabWhitePoint.X, Yr = LabWhitePoint.Y, Zr = LabWhitePoint.Z;

            double xr = input.X / Xr, yr = input.Y / Yr, zr = input.Z / Zr;

            double fx = f(xr);
            double fy = f(yr);
            double fz = f(zr);

            double L = 116 * fy - 16;
            double a = 500 * (fx - fy);
            double b = 200 * (fy - fz);

            var output = new LabColor(L, a, b, LabWhitePoint);
            return output;
        }

        private static double f(double cr)
        {
            double fc = cr > Epsilon ? Math.Pow(cr, 1 / 3d) : (Kappa * cr + 16) / 116d;
            return fc;
        }

        #region Overrides

        protected bool Equals(XYZToLabConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return LabWhitePoint.Equals(other.LabWhitePoint);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZToLabConverter) obj);
        }

        public override int GetHashCode()
        {
            return LabWhitePoint.GetHashCode();
        }

        public static bool operator ==(XYZToLabConverter left, XYZToLabConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZToLabConverter left, XYZToLabConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}