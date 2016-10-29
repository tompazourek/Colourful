#region License

// Copyright (C) Tomáš Pažourek, 2016
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

namespace Colourful
{
    /// <summary>
    /// Extensions useful for <see cref="LChabColor"/> and <see cref="LChuvColor"/> color spaces.
    /// </summary>
    internal static class SaturationLChFormulas
    {
        /// <summary>
        /// Returns saturation of the color (chroma normalized by lightness)
        /// </summary>
        public static double GetSaturation(double L, double C)
        {
            var result = 100*(C/L);

            if (double.IsNaN(result))
                return 0;

            return result;
        }

        /// <summary>
        /// Gets chroma from saturation and lightness
        /// </summary>
        public static double GetChroma(double saturation, double L)
        {
            var result = L*(saturation/100);
            return result;
        }
    }
}