#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
//
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion License

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
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new RGBToLinearRGBConverter();
            LinearRGBColor result = converter.Convert(color);
            return result;
        }

        public LinearRGBColor ToLinearRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion to linear RGB
            var xyzConverter = GetXYZToLinearRGBConverter(TargetRGBWorkingSpace);
            LinearRGBColor result = xyzConverter.Convert(adapted);
            return result;
        }

        public LinearRGBColor ToLinearRGB(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }

        public LinearRGBColor ToLinearRGB(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LinearRGBColor result = ToLinearRGB(xyzColor);
            return result;
        }
    }
}