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
        public LChuvColor ToLChuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            LuvColor luvColor = ToLuv(color);
            LChuvColor result = ToLChuv(luvColor);
            return result;
        }

        public LChuvColor ToLChuv(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }

        public LChuvColor ToLChuv(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation to target luv white point (LuvWhitePoint)
            LuvColor adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (perserving white point)
            var converter = new LuvToLChuvConverter();
            LChuvColor result = converter.Convert(adapted);
            return result;
        }

        public LChuvColor ToLChuv(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LChuvColor result = ToLChuv(xyzColor);
            return result;
        }
    }
}