using System;

namespace Colourful.RGBWorkingSpace
{
    /// <summary>
    /// Trivial implementation of <see cref="IRGBWorkingSpace" />
    /// </summary>
    public class RGBWorkingSpace : IRGBWorkingSpace, IEquatable<IRGBWorkingSpace>
    {
        /// <summary>
        /// Constructs RGB working space using a reference white, companding, and chromacity coordinates.
        /// </summary>
        public RGBWorkingSpace(in XYZColor referenceWhite, in ICompanding companding, in RGBPrimaries chromaticityCoordinates)
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
        public RGBPrimaries ChromaticityCoordinates { get; }

        /// <summary>
        /// Companding
        /// </summary>
        public ICompanding Companding { get; }

        #region Equality

        /// <inheritdoc />
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

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is IRGBWorkingSpace other && Equals(other);

        /// <inheritdoc />
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