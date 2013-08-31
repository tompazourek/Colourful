using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Best RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class BestRGB : IRGBWorkingSpace
    {
        public ICompanding Companding
        {
            get { return new GammaCompanding(2.2); }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D50; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.7347, 0.2653), G = new ChromaticityCoordinates(0.2150, 0.7750), B = new ChromaticityCoordinates(0.1300, 0.0350) }; }
        }
    }
}