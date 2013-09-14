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
    /// Best RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class BestRGB : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(2.2);
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.7347, 0.2653), G = new ChromaticityCoordinates(0.2150, 0.7750), B = new ChromaticityCoordinates(0.1300, 0.0350) };

        public ICompanding Companding
        {
            get { return _gammaCompanding; }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D50; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return _rgbPrimariesChromaticityCoordinates; }
        }
    }
}