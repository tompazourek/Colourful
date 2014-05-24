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

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 1976 formula
    /// </summary>
    public class CIE76ColorDifference : IColorDifference<LabColor>
    {
        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <returns>Delta-E (1976) color difference</returns>
        public double ComputeDifference(LabColor x, LabColor y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            if (x.WhitePoint != y.WhitePoint)
                throw new ArgumentException("Colors must have same white point to be compared.");

            var distance = (x.Vector - y.Vector).Norm(2);
            return distance;
        }
    }
}