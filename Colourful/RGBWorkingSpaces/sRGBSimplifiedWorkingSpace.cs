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
    /// sRGB simplified working space.
    /// Uses gamma for companding.
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class sRGBSimplifiedWorkingSpace : IRGBWorkingSpace
    {
        private readonly GammaCompanding _gammaCompanding = new GammaCompanding(2.2);

        public virtual XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D65; }
        }

        public virtual RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return sRGBWorkingSpace.ChromaticityCoordinatesConst; }
        }

        public ICompanding Companding
        {
            get { return _gammaCompanding; }
        }
    }
}