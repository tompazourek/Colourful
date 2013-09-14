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
    /// Ekta Space PS5
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class EktaSpacePS5 : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(2.2);
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6950, 0.3050), new ChromaticityCoordinates(0.2600, 0.7000), new ChromaticityCoordinates(0.1100, 0.0050));

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