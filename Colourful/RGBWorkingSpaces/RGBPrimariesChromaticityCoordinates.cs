using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Chromaticity coordinates of RGB primaries.
    /// One of the specifiers of <see cref="IRGBWorkingSpace"/>.
    /// </summary>
    public struct RGBPrimariesChromaticityCoordinates
    {
        /// <summary>
        /// Red
        /// </summary>
        public readonly ChromaticityCoordinates R;
        /// <summary>
        /// Green
        /// </summary>
        public readonly ChromaticityCoordinates G;
        /// <summary>
        /// Blue
        /// </summary>
        public readonly ChromaticityCoordinates B;

        public RGBPrimariesChromaticityCoordinates(ChromaticityCoordinates r, ChromaticityCoordinates g, ChromaticityCoordinates b)
        {
            R = r;
            G = g;
            B = b;
        }

        public bool Equals(RGBPrimariesChromaticityCoordinates other)
        {
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RGBPrimariesChromaticityCoordinates && Equals((RGBPrimariesChromaticityCoordinates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right)
        {
            return !left.Equals(right);
        }
    }
}