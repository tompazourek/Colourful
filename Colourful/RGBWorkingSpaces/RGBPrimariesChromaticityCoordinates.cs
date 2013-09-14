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
    }
}