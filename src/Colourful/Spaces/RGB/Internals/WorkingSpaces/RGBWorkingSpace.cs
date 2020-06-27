using System;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.Internals
{
    /// <inheritdoc cref="IRGBWorkingSpace" />
    public class RGBWorkingSpace : IRGBWorkingSpace, IEquatable<IRGBWorkingSpace>
    {
        /// <summary>
        /// Constructs RGB working space using a white point, companding, and primeries.
        /// </summary>
        public RGBWorkingSpace(in XYZColor whitePoint, in ICompanding companding, in RGBPrimaries primaries)
        {
            WhitePoint = whitePoint;
            Companding = companding;
            Primaries = primaries;
        }

        /// <inheritdoc />
        public XYZColor WhitePoint { get; }

        /// <inheritdoc />
        public RGBPrimaries Primaries { get; }

        /// <inheritdoc />
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
                   && Primaries.Equals(other.Primaries)
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
                hashCode = (hashCode * 397) ^ Primaries.GetHashCode();
                hashCode = (hashCode * 397) ^ (Companding != null ? Companding.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        [ExcludeFromCodeCoverage]
        public static bool operator ==(RGBWorkingSpace left, RGBWorkingSpace right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        [ExcludeFromCodeCoverage]
        public static bool operator !=(RGBWorkingSpace left, RGBWorkingSpace right) => !Equals(left, right);

        #endregion
    }
}