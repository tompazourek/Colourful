using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.RGBWorkingSpaces;

namespace Colourful.Colors
{
    /// <summary>
    /// RGB working color space.
    /// For more info see:
    /// </summary>
    public interface IRGBWorkingSpace
    {
        /// <summary>
        /// Reference white of the color space
        /// </summary>
        XYZColorBase ReferenceWhite { get; }

        /// <summary>
        /// Chromaticity coordinates of the primaries
        /// </summary>
        RGBPrimariesChromaticityCoordinates ChromaticityCoordinates { get; }

        /// <summary>
        /// Used for conversion to XYZ.
        /// The companded channel (R, G, B) is made linear with respect to the energy using this function.
        /// See this for more information:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </summary>
        IInverseCompanding InverseCompanding { get; }
    }
}