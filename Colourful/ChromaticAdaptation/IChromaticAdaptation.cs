using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// Chromatic adaptation.
    /// A linear transformation of a source color (XS, YS, ZS) into a destination color (XD, YD, ZD) by a linear transformation [M]
    /// which is dependent on the source reference white (XWS, YWS, ZWS) and the destination reference white (XWD, YWD, ZWD).
    /// See also:
    /// <seealso cref="BradfordChromaticAdaptation"/>,
    /// <seealso cref="VonKriesChromaticAdaptation"/>,
    /// <seealso cref="XYZScaling"/>.
    /// </summary>
    public interface IChromaticAdaptation
    {
        XYZColor Transform(XYZColor source, XYZColorBase destinationReferenceWhite);

        /// <remarks>Doesn't crop the resulting color space coordinates (e. g. allows negative values for XYZ coordinates).</remarks>
        XYZColorBase TransformNonCropped(XYZColor source, XYZColorBase destinationReferenceWhite);
    }
}