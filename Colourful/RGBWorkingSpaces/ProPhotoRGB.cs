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
    /// ProPhoto RGB
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class ProPhotoRGB : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(1.8);
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.7347, 0.2653), new ChromaticityCoordinates(0.1596, 0.8404), new ChromaticityCoordinates(0.0366, 0.0001));

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