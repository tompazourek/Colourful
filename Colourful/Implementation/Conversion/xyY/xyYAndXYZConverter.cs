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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Implementation.Conversion
{
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "xy"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xy")]
    public class xyYAndXYZConverter : IColorConversion<XYZColor, xyYColor>, IColorConversion<xyYColor, XYZColor>
    {
        public XYZColor Convert(xyYColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (input.y == 0)
                return new XYZColor(0, 0, input.Luminance);
            // ReSharper restore CompareOfFloatsByEqualityOperator

            double X = (input.x * input.Luminance) / input.y;
            double Y = input.Luminance;
            double Z = ((1 - input.x - input.y) * Y) / input.y;

            return new XYZColor(X, Y, Z);
        }

        public xyYColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            double x = input.X / (input.X + input.Y + input.Z);
            double y = input.Y / (input.X + input.Y + input.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(0, 0, input.Y);

            double Y = input.Y;
            return new xyYColor(x, y, Y);
        }
    }
}