using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// LMS color space represented by the response of the three types of cones of the human eye.
    /// </summary>
    public readonly struct LMSColor : IColorSpace, IColorVector, IEquatable<LMSColor>
    {
        #region Constructor

        /// <param name="l">L (usually from -1 to 1).</param>
        /// <param name="m">M (usually from -1 to 1).</param>
        /// <param name="s">S (usually from -1 to 1).</param>
        public LMSColor(in double l, in double m, in double s)
        {
            L = l;
            M = m;
            S = s;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1)</param>
        public LMSColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// Long wavelengths (red) cone response (Rho).
        /// Ranges usually from -1 to 1.
        /// </summary>
        public readonly double L;

        /// <summary>
        /// Medium wavelengths (green) cone response (Gamma).
        /// Ranges usually from -1 to 1.
        /// </summary>
        public readonly double M;

        /// <summary>
        /// Short wavelengths (blue) cone response (Beta).
        /// Ranges usually from -1 to 1.
        /// </summary>
        public readonly double S;

        /// <inheritdoc />
        public double[] Vector => new[] { L, M, S };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LMSColor other) =>
            L == other.L &&
            M == other.M &&
            S == other.S;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LMSColor other && Equals(other);

        /// <inheritdoc />
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
        public static bool operator ==(LMSColor left, LMSColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LMSColor left, LMSColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LMS [L={0:0.##}, M={1:0.##}, S={2:0.##}]", L, M, S);

        #endregion
    }
}