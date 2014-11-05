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

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        public LMSColor ToLMS(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var result = converter.Convert(color);
            return result;
        }

        public LMSColor ToLMS(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LMSColor result = ToLMS(xyzColor);
            return result;
        }
    }
}