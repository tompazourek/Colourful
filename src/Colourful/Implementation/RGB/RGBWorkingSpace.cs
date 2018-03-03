using System;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Trivial implementation of <see cref="IRGBWorkingSpace"/>
    /// </summary>
    public class RGBWorkingSpace : IRGBWorkingSpace
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
        protected bool Equals(IRGBWorkingSpace other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Equals(WhitePoint, other.WhitePoint) && ChromaticityCoordinates.Equals(other.ChromaticityCoordinates) && Equals(Companding, other.Companding);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var workingSpace = obj as IRGBWorkingSpace;
            if (workingSpace == null) return false;
            return Equals(workingSpace);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (WhitePoint != null ? WhitePoint.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ ChromaticityCoordinates.GetHashCode();
                hashCode = (hashCode*397) ^ (Companding != null ? Companding.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBWorkingSpace left, RGBWorkingSpace right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBWorkingSpace left, RGBWorkingSpace right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}