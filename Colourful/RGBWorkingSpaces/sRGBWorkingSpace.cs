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
    /// sRGB working space
    /// </summary>
    /// <remarks>
    /// Uses proper companding function, according to:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// <br />
    /// Chromaticity coordinates taken from:
    /// http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
    /// </remarks>
    public class sRGBWorkingSpace : IRGBWorkingSpace
    {
        internal static readonly RGBPrimariesChromaticityCoordinates ChromaticityCoordinatesConst = new RGBPrimariesChromaticityCoordinates
            (
                new ChromaticityCoordinates(0.6400, 0.3300),
                new ChromaticityCoordinates(0.3000, 0.6000),
                new ChromaticityCoordinates(0.1500, 0.0600)
            );

        private readonly sRGBCompanding _sRGBCompanding = new sRGBCompanding();

        public XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D65; }
        }

        public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
        {
            get { return ChromaticityCoordinatesConst; }
        }

        public ICompanding Companding
        {
            get { return _sRGBCompanding; }
        }
    }
}