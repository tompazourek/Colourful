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
    /// Don RGB 4
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class DonRGB4 : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(2.2);
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6960, 0.3000), new ChromaticityCoordinates(0.2150, 0.7650), new ChromaticityCoordinates(0.1300, 0.0350));

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