using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE L*u*v* (1976) color.
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct LuvColor : IColorSpace, IColorVector, IEquatable<LuvColor>
    {
        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100).</param>
        /// <param name="u">u* (usually from -100 to 100).</param>
        /// <param name="v">v* (usually from -100 to 100).</param>
        public LuvColor(in double l, in double u, in double v)
        {
            L = l;
            this.u = u;
            this.v = v;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
        public LuvColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness).
        /// Ranges from 0 to 100.
        /// </summary>
        public readonly double L;

        /// <summary>
        /// u*.
        /// Ranges usually from -100 to 100.
        /// </summary>
        public readonly double u;

        /// <summary>
        /// v*.
        /// Ranges usually from -100 to 100.
        /// </summary>
        public readonly double v;

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { L, u, v };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LuvColor other) =>
            L == other.L &&
            u == other.u &&
            v == other.v;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LuvColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ u.GetHashCode();
                hashCode = (hashCode * 397) ^ v.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(LuvColor left, LuvColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(LuvColor left, LuvColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Luv [L={0:0.##}, u={1:0.##}, v={2:0.##}]", L, u, v);

        #endregion
    }
}