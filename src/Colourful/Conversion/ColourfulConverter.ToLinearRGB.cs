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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public LinearRGBColor ToLinearRGB(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new RGBToLinearRGBConverter();
            var result = converter.Convert(color);
            return result;
        }

        public LinearRGBColor ToLinearRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation
            var adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion to linear RGB
            var xyzConverter = GetXYZToLinearRGBConverter(TargetRGBWorkingSpace);
            var result = xyzConverter.Convert(adapted);
            return result;
        }

        public LinearRGBColor ToLinearRGB(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

#if (DYNAMIC)
        public LinearRGBColor ToLinearRGB<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LinearRGBColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLinearRGB(source);
            }
        }
#endif
    }
}