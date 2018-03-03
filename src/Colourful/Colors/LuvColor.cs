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
    /// CIE L*u*v* (1976) color
    /// </summary>
    public class LuvColor : IColorVector
    {
        /// <summary>
        /// D65 standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D65;

        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="u">u* (usually from -100 to 100)</param>
        /// <param name="v">v* (usually from -100 to 100)</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "u"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "v")]
        public LuvColor(double l, double u, double v) : this(l, u, v, DefaultWhitePoint)
        {
        }

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="u">u* (usually from -100 to 100)</param>
        /// <param name="v">v* (usually from -100 to 100)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "u"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "v")]
        public LuvColor(double l, double u, double v, XYZColor whitePoint)
        {
            L = l;
            this.u = u;
            this.v = v;
            WhitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        public LuvColor(Vector vector) : this(vector, DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public LuvColor(Vector vector, XYZColor whitePoint)
            : this(vector[0], vector[1], vector[2], whitePoint)
        {
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
        public double L { get; }

        /// <summary>
        /// u*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "u"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "u")]
        public double u { get; }

        /// <summary>
        /// v*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "v"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "v")]
        public double v { get; }

        /// <remarks><see cref="Illuminants"/></remarks>
        public XYZColor WhitePoint { get; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector => new[] { L, u, v };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(LuvColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return L.Equals(other.L) && u.Equals(other.u) && v.Equals(other.v);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LuvColor)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode*397) ^ u.GetHashCode();
                hashCode = (hashCode*397) ^ v.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LuvColor left, LuvColor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LuvColor left, LuvColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Luv [L={0:0.##}, u={1:0.##}, v={2:0.##}]", L, u, v);
        }

        #endregion
    }
}