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
        public LuvColor ToLuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetLuvWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLuvWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLuvConverter(TargetLuvWhitePoint);
            LuvColor result = converter.Convert(adapted);
            return result;
        }

        public LuvColor ToLuv(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }

        public LuvColor ToLuv(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion (perserving white point)
            var converter = new LChuvToLuvConverter();
            LuvColor unadapted = converter.Convert(color);

            if (!IsChromaticAdaptationPerformed)
                return unadapted;

            // adaptation to target luv white point (LuvWhitePoint)
            LuvColor adapted = Adapt(unadapted);
            return adapted;
        }

        public LuvColor ToLuv(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LuvColor result = ToLuv(xyzColor);
            return result;
        }
    }
}