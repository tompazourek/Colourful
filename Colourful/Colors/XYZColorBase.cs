using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// CIE 1931 XYZ without reference white.
    /// See also: <seealso cref="XYZColor"/>.
    /// </summary>
    public class XYZColorBase : IColorVector
    {
        #region Constructor

        /// <param name="x">X (usually from 0 to 1)</param>
        /// <param name="y">Y (usually from 0 to 1)</param>
        /// <param name="z">Z (usually from 0 to 1)</param>
        internal XYZColorBase(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// For <see cref="XYZColor"/>, the range is restricted by <see cref="XYZColor.ReferenceWhite"/>.
        /// </remarks>
        public double X { get; private set; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// For <see cref="XYZColor"/>, the range is restricted by <see cref="XYZColor.ReferenceWhite"/>.
        /// </remarks>
        public double Y { get; private set; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// For <see cref="XYZColor"/>, the range is restricted by <see cref="XYZColor.ReferenceWhite"/>.
        /// </remarks>
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

        protected bool Equals(XYZColorBase other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZColorBase) obj);
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

        public static bool operator ==(XYZColorBase left, XYZColorBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZColorBase left, XYZColorBase right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}