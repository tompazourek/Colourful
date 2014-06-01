#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
        /// <remarks>Doesn't crop the resulting color space coordinates (e. g. allows negative values for XYZ coordinates).</remarks>
        XYZColor Transform(XYZColor sourceColor, XYZColor sourceWhitePoint, XYZColor targetWhitePoint);
    }
}