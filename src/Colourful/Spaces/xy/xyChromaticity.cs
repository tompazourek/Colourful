using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE xy chromaticity space.
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct xyChromaticity : IColorSpace, IColorVector, IEquatable<xyChromaticity>
    {
        /// <param name="x">Chromaticity x (usually from 0 to 1).</param>
        /// <param name="y">Chromaticity y (usually from 0 to 1).</param>
        public xyChromaticity(in double x, in double y)
        {
            this.x = x;
            this.y = y;
        }
        
        /// <param name="vector"><see cref="Vector" />, expected 2 dimensions.</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
        public xyChromaticity(in double[] vector)
            : this(in vector[0], in vector[1])
        {
        }

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

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { x, y };

        #region Equality

        /// <inheritdoc />
        public bool Equals(xyChromaticity other) => x.Equals(other.x) && y.Equals(other.y);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is xyChromaticity xyChromaticity && Equals(xyChromaticity);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(xyChromaticity left, xyChromaticity right) => left.Equals(right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(xyChromaticity left, xyChromaticity right) => !left.Equals(right);

        #endregion
        
        #region Deconstructor

        /// <summary>
        /// Deconstructs color into individual channels.
        /// </summary>
        public void Deconstruct(out double x, out double y)
        {
            x = this.x;
            y = this.y;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);

        #endregion
    }
}