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
        public HunterLabColor ToHunterLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation
            var adapted = !WhitePoint.Equals(TargetHunterLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetHunterLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToHunterLabConverter(TargetHunterLabWhitePoint);
            var result = converter.Convert(adapted);
            return result;
        }

        public HunterLabColor ToHunterLab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

#if (DYNAMIC)
        public HunterLabColor ToHunterLab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as HunterLabColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToHunterLab(source);
            }
        }
#endif
    }
}