using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Conversion;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// CIE-L*C*h, cylindrical form of CIE L*a*b*
    /// </summary>
    public class LChabColor : IColorVector
    {
        #region Constructor

        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <param name="h">Hue in degrees (not radians)</param>
        public LChabColor(double l, double c, double h)
        {
            L = l;
            C = c;
            this.h = h;
        }

        #endregion

        #region Channels

        public double L { get; private set; }
        public double C { get; private set; }

        /// <summary>
        /// Hue in degrees (not radians)
        /// </summary>
        public double h { get; private set; }

        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { L, C, h }); }
        }

        #endregion

        #region Equality

        protected bool Equals(LChabColor other)
        {
            return L.Equals(other.L) && C.Equals(other.C) && h.Equals(other.h);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LChabColor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ C.GetHashCode();
                hashCode = (hashCode * 397) ^ h.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LChabColor left, LChabColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LChabColor left, LChabColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Conversions

        #region From

        public static LChabColor FromLab(LabColor input)
        {
            return input.ToLChab();
        }

        #endregion

        #region To

        public LabColor ToLab()
        {
            var converter = new LabAndLChabConverter();
            var result = converter.Convert(this);
            return result;
        }

        #endregion

        #endregion

    }
}