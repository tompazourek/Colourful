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
    /// ColorMatch RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class ColorMatchRGB : IRGBWorkingSpace
    {
        public IInverseCompanding InverseCompanding
        {
            get { return new InverseGammaCompanding(1.8); }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D50; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.6300, 0.3400), G = new ChromaticityCoordinates(0.2950, 0.6050), B = new ChromaticityCoordinates(0.1500, 0.0750) }; }
        }
    }
}