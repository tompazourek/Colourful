using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Conversion;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// CIE L*C*h°, cylindrical form of <see cref="LabColor">CIE L*a*b* (1976)</see>
    /// </summary>
    public class LChabColor : IColorVector
    {
        #region Constructor

        /// <param name="l">L* (lightness)</param>
        /// <param name="c">C* (chroma)</param>
        /// <param name="h">h° (hue in degrees)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LChabColor(double l, double c, double h)
        {
            L = l;
            C = c;
            this.h = h;
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L")]
        public double L { get; private set; }

        /// <summary>
        /// C* (chroma)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        public double C { get; private set; }

        /// <summary>
        /// h° (hue in degrees)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "h"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h")]
        public double h { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { L, C, h }); }
        }

        #endregion

        #region Equality

        public bool Equals(LChabColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return L.Equals(other.L) && C.Equals(other.C) && h.Equals(other.h);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LChabColor) obj);
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
            if (input == null) throw new ArgumentNullException("input");

            return input.ToLChab();
        }

        #endregion

        #region To

        public LabColor ToLab()
        {
            var converter = new LabAndLChabConverter();
            LabColor result = converter.Convert(this);
            return result;
        }

        #endregion

        #endregion
    }
}