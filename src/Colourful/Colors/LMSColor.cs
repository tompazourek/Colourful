using System;
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
    /// LMS color space represented by the response of the three types of cones of the human eye
    /// </summary>
    public class LMSColor : IColorVector
    {
        #region Constructor

        /// <param name="l">L (usually from -1 to 1)</param>
        /// <param name="m">M (usually from -1 to 1)</param>
        /// <param name="s">S (usually from -1 to 1)</param>
        public LMSColor(double l, double m, double s)
        {
            L = l;
            M = m;
            S = s;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1)</param>
        public LMSColor(Vector vector)
            : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// Long wavelengths (red) cone response (Rho)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        public double L { get; }

        /// <summary>
        /// Medium wavelengths (green) cone response (Gamma)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        public double M { get; }

        /// <summary>
        /// Short wavelengths (blue) cone response (Beta)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        public double S { get; }

        /// <summary>
        ///     <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { L, M, S };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(LMSColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return L.Equals(other.L) && M.Equals(other.M) && S.Equals(other.S);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LMSColor)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ M.GetHashCode();
                hashCode = (hashCode * 397) ^ S.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LMSColor left, LMSColor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LMSColor left, LMSColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "LMS [L={0:0.##}, M={1:0.##}, S={2:0.##}]", L, M, S);
        }

        #endregion
    }
}