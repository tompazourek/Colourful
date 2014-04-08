using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// CIE 1931 XYZ color space
    /// </summary>
    public class XYZColor : IColorVector
    {
        #region Constructor

        /// <param name="x">X (usually from 0 to 1)</param>
        /// <param name="y">Y (usually from 0 to 1)</param>
        /// <param name="z">Z (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        public XYZColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X")]
        public double X { get; private set; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
        public double Y { get; private set; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z")]
        public double Z { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { X, Y, Z }); }
        }

        #endregion

        #region Equality

        public bool Equals(XYZColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(XYZColor left, XYZColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZColor left, XYZColor right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}