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
using System.Threading.Tasks;

namespace Colourful.Implementation
{
    /// <summary>
    /// Angle unit conversion helpers
    /// </summary>
    internal static class Angle
    {
        private const double TwoPI = 2 * Math.PI;

        public static double RadianToDegree(double rad)
        {
            double deg = 360 * (rad / TwoPI);
            return deg;
        }

        public static double DegreeToRadian(double deg)
        {
            double rad = TwoPI * (deg / 360d);
            return rad;
        }
    }
}