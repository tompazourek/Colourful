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
        public ChromaticityCoordinates B;
        public ChromaticityCoordinates G;
        public ChromaticityCoordinates R;
    }
}