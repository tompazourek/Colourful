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
    /// Wide Gamut RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class WideGamutsRGB : IRGBWorkingSpace
    {
        public IInverseCompanding InverseCompanding
        {
            get { return new InverseGammaCompanding(2.2); }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D50; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.7350, 0.2650), G = new ChromaticityCoordinates(0.1150, 0.8260), B = new ChromaticityCoordinates(0.1570, 0.0180) }; }
        }
    }
}