using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor"/> to <see cref="LuvColor"/>.
    /// </summary>
    public class XYZToLuvConverter : IColorConversion<XYZColor, LuvColor>
    {
        public XYZToLuvConverter()
            : this(LuvColor.DefaultWhitePoint)
        {
        }

        public XYZToLuvConverter(XYZColor labWhitePoint)
        {
            LuvWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LuvColor.DefaultWhitePoint"/> is used.
        /// </summary>
        public XYZColor LuvWhitePoint { get; }

        public LuvColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Luv.html

            var yr = input.Y/LuvWhitePoint.Y;
            var up = Compute_up(input);
            var vp = Compute_vp(input);
            var upr = Compute_up(LuvWhitePoint);
            var vpr = Compute_vp(LuvWhitePoint);

            var L = yr > CIEConstants.Epsilon ? (116*Math.Pow(yr, 1/3d) - 16) : (CIEConstants.Kappa*yr);

            if (double.IsNaN(L) || L < 0)
                L = 0;

            var u = 13*L*(up - upr);
            var v = 13*L*(vp - vpr);

            if (double.IsNaN(u))
                u = 0;

            if (double.IsNaN(v))
                v = 0;

            return new LuvColor(L, u, v, LuvWhitePoint);
        }

        private static double Compute_up(XYZColor input)
        {
            return (4*input.X)/(input.X + 15*input.Y + 3*input.Z);
        }

        private static double Compute_vp(XYZColor input)
        {
            return (9*input.Y)/(input.X + 15*input.Y + 3*input.Z);
        }

        #region Overrides

        protected bool Equals(XYZToLuvConverter other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return LuvWhitePoint.Equals(other.LuvWhitePoint);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZToLuvConverter)obj);
        }

        public override int GetHashCode()
        {
            return LuvWhitePoint.GetHashCode();
        }

        public static bool operator ==(XYZToLuvConverter left, XYZToLuvConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZToLuvConverter left, XYZToLuvConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}