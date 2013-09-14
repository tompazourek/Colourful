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
    /// CIE L*a*b* (1976) color
    /// </summary>
    public class LabColor : IColorVector
    {
        #region Constructor

        /// <param name="l">L* (lightness)</param>
        /// <param name="a">a*</param>
        /// <param name="b">b*</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LabColor(double l, double a, double b)
        {
            L = l;
            this.a = a;
            this.b = b;
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
        /// a*
        /// </summary>
        /// <remarks>
        /// Negative values indicate green while positive values indicate magenta.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public double a { get; private set; }

        /// <summary>
        /// b*
        /// </summary>
        /// <remarks>
        /// Negative values indicate blue and positive values indicate yellow.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
        public double b { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { L, a, b }); }
        }

        #endregion

        #region Equality

        public bool Equals(LabColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return L.Equals(other.L) && a.Equals(other.a) && b.Equals(other.b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabColor) obj);
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
            if (input == null) throw new ArgumentNullException("input");

            return input.ToLab();
        }

        public static LabColor FromLChab(LChabColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            return input.ToLab();
        }

        #endregion

        #region To

        /// <remarks>
        /// Target reference white is <see cref="XYZColor.DefaultReferenceWhite"/>.
        /// </remarks>
        public XYZColor ToXYZ()
        {
            var converter = new LabToXYZConverter();
            XYZColor result = converter.Convert(this);
            return result;
        }

        /// <remarks>
        /// Target XYZ color has given reference white.
        /// </remarks>
        public XYZColor ToXYZ(XYZColorBase referenceWhite)
        {
            var converter = new LabToXYZConverter(referenceWhite);
            XYZColor result = converter.Convert(this);
            return result;
        }

        public LChabColor ToLChab()
        {
            var converter = new LabAndLChabConverter();
            LChabColor result = converter.Convert(this);
            return result;
        }

        #endregion

        #endregion
    }
}