using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public readonly struct xyChromaticityCoordinates
    {
        /// <param name="x">Chromaticity coordinate x (usually from 0 to 1)</param>
        /// <param name="y">Chromaticity coordinate y (usually from 0 to 1)</param>
        public xyChromaticityCoordinates(double x, double y)
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

        /// <inheritdoc cref="object" />
        public bool Equals(xyChromaticityCoordinates other) => x.Equals(other.x) && y.Equals(other.y);

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is xyChromaticityCoordinates coordinates && Equals(coordinates);

        /// <inheritdoc cref="object" />
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

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);

        #endregion
    }
}