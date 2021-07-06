using System;

namespace Colourful.Internals
{
    /// <summary>
    /// Chromaticity of RGB primaries.
    /// </summary>
    public readonly struct RGBPrimaries : IEquatable<RGBPrimaries>
    {
        /// <summary>
        /// Constructs RGB primaries.
        /// </summary>
        public RGBPrimaries(in xyChromaticity r, in xyChromaticity g, in xyChromaticity b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Red.
        /// </summary>
        public xyChromaticity R { get; }

        /// <summary>
        /// Green.
        /// </summary>
        public xyChromaticity G { get; }

        /// <summary>
        /// Blue.
        /// </summary>
        public xyChromaticity B { get; }

        #region Equality

        /// <inheritdoc />
        public bool Equals(RGBPrimaries other) =>
            R.Equals(other.R) &&
            G.Equals(other.G) &&
            B.Equals(other.B);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is RGBPrimaries other && Equals(other);

        /// <inheritdoc />
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
#if !NETSTANDARD1_1
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(RGBPrimaries left, RGBPrimaries right) => left.Equals(right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(RGBPrimaries left, RGBPrimaries right) => !left.Equals(right);

        #endregion
    }
}
