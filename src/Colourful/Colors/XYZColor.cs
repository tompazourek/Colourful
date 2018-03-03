using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

#endif

namespace Colourful
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "z")]
        public XYZColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public XYZColor(Vector vector)
            : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X")]
        public double X { get; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y")]
        public double Y { get; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z")]
        public double Z { get; }

        /// <summary>
        ///     <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { X, Y, Z };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(XYZColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZColor)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZColor left, XYZColor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZColor left, XYZColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "XYZ [X={0:0.##}, Y={1:0.##}, Z={2:0.##}]", X, Y, Z);
        }

        #endregion
    }
}