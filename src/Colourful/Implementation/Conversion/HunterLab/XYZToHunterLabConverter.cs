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

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="HunterLabColor"/> to <see cref="XYZColor"/>.
    /// </summary>
    public class XYZToHunterLabConverter : XYZAndHunterLabConverterBase, IColorConversion<XYZColor, HunterLabColor>
    {
        public XYZToHunterLabConverter()
            : this(HunterLabColor.DefaultWhitePoint)
        {
        }

        public XYZToHunterLabConverter(XYZColor labWhitePoint)
        {
            HunterLabWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LabColor.DefaultWhitePoint"/> is used.
        /// </summary>
        public XYZColor HunterLabWhitePoint { get; }

        public HunterLabColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            // conversion algorithm described here: http://en.wikipedia.org/wiki/Lab_color_space#Hunter_Lab
            double X = input.X, Y = input.Y, Z = input.Z;
            double Xn = HunterLabWhitePoint.X, Yn = HunterLabWhitePoint.Y, Zn = HunterLabWhitePoint.Z;

            var Ka = ComputeKa(HunterLabWhitePoint);
            var Kb = ComputeKb(HunterLabWhitePoint);

            var L = 100*Math.Sqrt(Y/Yn);
            var a = Ka*((X/Xn - Y/Yn)/Math.Sqrt(Y/Yn));
            var b = Kb*((Y/Yn - Z/Zn)/Math.Sqrt(Y/Yn));

            if (double.IsNaN(a))
                a = 0;

            if (double.IsNaN(b))
                b = 0;

            var output = new HunterLabColor(L, a, b, HunterLabWhitePoint);
            return output;
        }
    }
}