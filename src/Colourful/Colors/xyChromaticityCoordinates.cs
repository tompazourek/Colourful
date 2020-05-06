using System;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public readonly struct xyChromaticityCoordinates : IEquatable<xyChromaticityCoordinates>
    {
        /// <param name="x">Chromaticity coordinate x (usually from 0 to 1)</param>
        /// <param name="y">Chromaticity coordinate y (usually from 0 to 1)</param>
        public xyChromaticityCoordinates(in double x, in double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Chromaticity coordinate x
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double x { get; }

        /// <summary>
        /// Chromaticity coordinate y
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double y { get; }

        #region Equality

        /// <inheritdoc />
        public bool Equals(xyChromaticityCoordinates other) => x.Equals(other.x) && y.Equals(other.y);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is xyChromaticityCoordinates coordinates && Equals(coordinates);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(xyChromaticityCoordinates left, xyChromaticityCoordinates right) => left.Equals(right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(xyChromaticityCoordinates left, xyChromaticityCoordinates right) => !left.Equals(right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);

        #endregion
    }
}