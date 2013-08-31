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
    /// CIE RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class CIERGB : IRGBWorkingSpace
    {
        public ICompanding Companding
        {
            get { return new GammaCompanding(2.2); }
        }

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.E; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.7350, 0.2650), G = new ChromaticityCoordinates(0.2740, 0.7170), B = new ChromaticityCoordinates(0.1670, 0.0090) }; }
        }
    }
}