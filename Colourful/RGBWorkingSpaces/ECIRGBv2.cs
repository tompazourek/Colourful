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
    /// ECI RGB v2
    /// </summary>
    /// <remarks>
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class ECIRGBv2 : IRGBWorkingSpace
    {
        private readonly LCompanding _lCompanding = new LCompanding();
        private readonly RGBPrimariesChromaticityCoordinates _rgbPrimariesChromaticityCoordinates = new RGBPrimariesChromaticityCoordinates { R = new ChromaticityCoordinates(0.6700, 0.3300), G = new ChromaticityCoordinates(0.2100, 0.7100), B = new ChromaticityCoordinates(0.1400, 0.0800) };

        public ICompanding Companding
        {
            get { return _lCompanding; }
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