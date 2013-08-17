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
    /// Ekta Space PS5
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class EktaSpacePS5 : IRGBWorkingSpace
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
            get { return new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.6950, 0.3050), G = new ChromaticityCoordinates(0.2600, 0.7000), B = new ChromaticityCoordinates(0.1100, 0.0050) }; }
        }
    }
}