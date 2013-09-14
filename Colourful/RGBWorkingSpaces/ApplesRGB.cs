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
    /// Apple sRGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class ApplesRGB : IRGBWorkingSpace
    {
        public ICompanding Companding
        {
            get { return new GammaCompanding(1.8); }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D65; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.6250, 0.3400), G = new ChromaticityCoordinates(0.2800, 0.5950), B = new ChromaticityCoordinates(0.1550, 0.0700) }; }
        }
    }
}