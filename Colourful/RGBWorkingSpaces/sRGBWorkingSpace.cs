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
            {
                R = new ChromaticityCoordinates(0.6400, 0.3300),
                G = new ChromaticityCoordinates(0.3000, 0.6000),
                B = new ChromaticityCoordinates(0.1500, 0.0600),
            };

        public XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D65; }
        }

        public RGBPrimariesChromaticityCoordinates _chromaticityCoordinates
        {
            get { return ChromaticityCoordinatesConst; }
        }

        public IInverseCompanding InverseCompanding
        {
            get { return new sRGBInverseCompanding(); }
        }
    }
}