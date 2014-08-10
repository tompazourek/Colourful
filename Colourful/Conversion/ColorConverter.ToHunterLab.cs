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
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColorConverter
    {
        public HunterLabColor ToHunterLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetHunterLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetHunterLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToHunterLabConverter(TargetHunterLabWhitePoint);
            HunterLabColor result = converter.Convert(adapted);
            return result;
        }

        public HunterLabColor ToHunterLab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }

        public HunterLabColor ToHunterLab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            HunterLabColor result = ToHunterLab(xyzColor);
            return result;
        }
    }
}