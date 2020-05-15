using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE L*a*b* (1976) color.
    /// </summary>
    public readonly struct LabColor : IColorSpace, IColorVector, IEquatable<LabColor>
    {
        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100).</param>
        /// <param name="a">a* (usually from -100 to 100).</param>
        /// <param name="b">b* (usually from -100 to 100).</param>
        public LabColor(in double l, in double a, in double b)
        {
            L = l;
            this.a = a;
            this.b = b;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
        public LabColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness).
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public readonly double L;

        /// <summary>
        /// a*.
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate green while positive values indicate magenta.
        /// </remarks>
        public readonly double a;

        /// <summary>
        /// b*.
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate blue and positive values indicate yellow.
        /// </remarks>
        public readonly double b;

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public double[] Vector => new[] { L, a, b };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LabColor other) =>
            L == other.L &&
            a == other.a &&
            b == other.b;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LabColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LabColor left, LabColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LabColor left, LabColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Lab [L={0:0.##}, a={1:0.##}, b={2:0.##}]", L, a, b);

        #endregion
    }
}