using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="HunterLabColor"/> to <see cref="XYZColor"/>.
    /// </summary>
    public class HunterLabToXYZConverter : XYZAndHunterLabConverterBase, IColorConversion<HunterLabColor, XYZColor>
    {
        public XYZColor Convert(HunterLabColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            double L = input.L, a = input.a, b = input.b;
            double Xn = input.WhitePoint.X, Yn = input.WhitePoint.Y, Zn = input.WhitePoint.Z;

            double Ka = ComputeKa(input.WhitePoint);
            double Kb = ComputeKb(input.WhitePoint);

            double Y = Math.Pow(L / 100d, 2) * Yn;
            double X = ((a / Ka) * Math.Sqrt(Y / Yn) + Y / Yn) * Xn;
            double Z = ((b / Kb) * Math.Sqrt(Y / Yn) - Y / Yn) * (-Zn);

            var result = new XYZColor(X, Y, Z);
            return result;
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public static bool operator ==(HunterLabToXYZConverter left, HunterLabToXYZConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HunterLabToXYZConverter left, HunterLabToXYZConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}