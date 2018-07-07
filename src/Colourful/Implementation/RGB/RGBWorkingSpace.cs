namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Trivial implementation of <see cref="IRGBWorkingSpace" />
    /// </summary>
    public sealed class RGBWorkingSpace : IRGBWorkingSpace
    {
        /// <summary>
        /// Constructs RGB working space using a reference white, companding, and chromacity coordinates.
        /// </summary>
        public RGBWorkingSpace(XYZColor referenceWhite, ICompanding companding, RGBPrimariesChromaticityCoordinates chromaticityCoordinates)
        {
            WhitePoint = referenceWhite;
            Companding = companding;
            ChromaticityCoordinates = chromaticityCoordinates;
        }

        /// <summary>
        /// Reference white point
        /// </summary>
        public XYZColor WhitePoint { get; }

        /// <summary>
        /// Chromacity coordinates
        /// </summary>
        public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates { get; }

        /// <summary>
        /// Companding
        /// </summary>
        public ICompanding Companding { get; }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(IRGBWorkingSpace other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Equals(WhitePoint, other.WhitePoint)
                   && ChromaticityCoordinates.Equals(other.ChromaticityCoordinates)
                   && Companding.Equals(other.Companding);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is IRGBWorkingSpace other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = WhitePoint.GetHashCode();
                hashCode = (hashCode * 397) ^ ChromaticityCoordinates.GetHashCode();
                hashCode = (hashCode * 397) ^ (Companding != null ? Companding.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBWorkingSpace left, RGBWorkingSpace right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBWorkingSpace left, RGBWorkingSpace right) => !Equals(left, right);

        #endregion
    }
}