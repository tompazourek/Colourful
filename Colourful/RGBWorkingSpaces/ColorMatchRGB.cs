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
    /// ColorMatch RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class ColorMatchRGB : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(1.8);
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.6300, 0.3400), G = new ChromaticityCoordinates(0.2950, 0.6050), B = new ChromaticityCoordinates(0.1500, 0.0750) };

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