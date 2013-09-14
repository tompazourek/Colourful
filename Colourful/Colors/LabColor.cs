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
    /// CIE 1976 L*a*b*
    /// </summary>
    public class LabColor : IColorVector
    {
        #region Constructor

        public LabColor(double l, double a, double b)
        {
            L = l;
            this.a = a;
            this.b = b;
        }

        #endregion

        #region Channels

        public double L { get; private set; }
        public double a { get; private set; }
        public double b { get; private set; }

        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { L, a, b }); }
        }

        #endregion

        #region Equality

        protected bool Equals(LabColor other)
        {
            return L.Equals(other.L) && a.Equals(other.a) && b.Equals(other.b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabColor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LabColor left, LabColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LabColor left, LabColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Conversions

        #region From

        public static LabColor FromXYZ(XYZColor input)
        {
            return input.ToLab();
        }

        public static LabColor FromLChab(LChabColor input)
        {
            return input.ToLab();
        }

        #endregion

        #region To

        /// <remarks>
        /// Target reference white is <see cref="XYZColor.DefaultReferenceWhite"/>.
        /// </remarks>
        public XYZColor ToXYZ()
        {
            var converter = new XYZAndLabConverter();
            var result = converter.Convert(this);
            return result;
        }

        /// <remarks>
        /// Target XYZ color has given reference white.
        /// </remarks>
        public XYZColor ToXYZ(XYZColorBase referenceWhite)
        {
            var converter = new XYZAndLabConverter();
            var result = converter.Convert(this, referenceWhite);
            return result;
        }

        public LChabColor ToLChab()
        {
            var converter = new LabAndLChabConverter();
            var result = converter.Convert(this);
            return result;
        }

        #endregion

        #endregion
    }
}