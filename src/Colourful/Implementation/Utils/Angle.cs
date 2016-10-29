﻿#region License

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

namespace Colourful.Implementation
{
    /// <summary>
    /// Angle unit conversion helpers
    /// </summary>
    internal static class Angle
    {
        private const double TwoPI = 2*Math.PI;

        public static double RadianToDegree(double rad)
        {
            var deg = 360*(rad/TwoPI);
            return deg;
        }

        public static double DegreeToRadian(double deg)
        {
            var rad = TwoPI*(deg/360d);
            return rad;
        }
    }
}