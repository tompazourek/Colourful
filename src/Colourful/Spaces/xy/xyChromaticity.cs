using System;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE xy chromaticity space.
    /// </summary>
    public readonly struct xyChromaticity : IColorVector, IEquatable<xyChromaticity>
    {
        /// <param name="x">Chromaticity x (usually from 0 to 1).</param>
        /// <param name="y">Chromaticity y (usually from 0 to 1).</param>
        public xyChromaticity(in double x, in double y)
        {
            this.x = x;
            this.y = y;
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
        public static bool operator ==(xyChromaticity left, xyChromaticity right) => left.Equals(right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(xyChromaticity left, xyChromaticity right) => !left.Equals(right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);

        #endregion
    }
}