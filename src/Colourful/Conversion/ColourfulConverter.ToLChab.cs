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
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        public LChabColor ToLChab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var labColor = ToLab(color);
            var result = ToLChab(labColor);
            return result;
        }

        public LChabColor ToLChab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation to target lab white point (LabWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = new LabToLChabConverter();
            var result = converter.Convert(adapted);
            return result;
        }

        public LChabColor ToLChab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        public LChabColor ToLChab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

#if (DYNAMIC)
        public LChabColor ToLChab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LChabColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLChab(source);
            }
        }
#endif
    }
}