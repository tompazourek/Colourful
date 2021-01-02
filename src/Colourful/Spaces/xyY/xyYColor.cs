using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE xyY color space (derived from <see cref="XYZColor" /> color space).
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct xyYColor : IColorSpace, IColorVector, IEquatable<xyYColor>
    {
        #region Constructor

        /// <param name="x">x (usually from 0 to 1) chromaticity.</param>
        /// <param name="y">y (usually from 0 to 1) chromaticity.</param>
        /// <param name="Y">Y (luminance) (usually from 0 to 1).</param>
        public xyYColor(in double x, in double y, in double Y)
        {
            this.x = x;
            this.y = y;
            Luminance = Y;
        }

        /// <param name="chromaticity">Chromaticity (x and y together).</param>
        /// <param name="Y">Y (luminance) (usually from 0 to 1).</param>
        public xyYColor(in xyChromaticity chromaticity, in double Y)
            : this(in chromaticity.x, in chromaticity.y, in Y)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1).</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
        public xyYColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// Chromaticity x.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double x;

        /// <summary>
        /// Chromaticity y.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double y;

        /// <summary>
        /// Y channel (luminance).
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double Luminance;

        /// <summary>
        /// Chromaticity (identical to x and y).
        /// </summary>
        public xyChromaticity Chromaticity => new xyChromaticity(in x, in y);

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { x, y, Luminance };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(xyYColor other) =>
            x == other.x &&
            y == other.y &&
            Luminance == other.Luminance;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is xyYColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ Luminance.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(xyYColor left, xyYColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(xyYColor left, xyYColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xyY [x={0:0.##}, y={1:0.##}, Y={2:0.##}]", x, y, Luminance);

        #endregion
    }
}