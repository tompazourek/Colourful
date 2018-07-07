namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Chromaticity coordinates of RGB primaries.
    /// One of the specifiers of <see cref="IRGBWorkingSpace" />.
    /// </summary>
    public readonly struct RGBPrimariesChromaticityCoordinates
    {
        /// <summary>
        /// Constructs coordinates
        /// </summary>
        public RGBPrimariesChromaticityCoordinates(xyChromaticityCoordinates r, xyChromaticityCoordinates g, xyChromaticityCoordinates b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Red
        /// </summary>
        public xyChromaticityCoordinates R { get; }

        /// <summary>
        /// Green
        /// </summary>
        public xyChromaticityCoordinates G { get; }

        /// <summary>
        /// Blue
        /// </summary>
        public xyChromaticityCoordinates B { get; }

        /// <inheritdoc cref="object" />
        public bool Equals(RGBPrimariesChromaticityCoordinates other) =>
            R.Equals(other.R) &&
            G.Equals(other.G) &&
            B.Equals(other.B);

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is RGBPrimariesChromaticityCoordinates other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right) => left.Equals(right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right) => !left.Equals(right);
    }
}