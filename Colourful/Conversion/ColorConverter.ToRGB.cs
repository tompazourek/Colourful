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
using System.Threading.Tasks;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColorConverter
    {
        private XYZToRGBConverter _lastXYZToRGBConverter;

        private XYZToRGBConverter GetXYZToRGBConverter(IRGBWorkingSpace workingSpace)
        {
            if (_lastXYZToRGBConverter != null &&
                _lastXYZToRGBConverter.TargetRGBWorkingSpace.Equals(workingSpace))
                return _lastXYZToRGBConverter;

            return _lastXYZToRGBConverter = new XYZToRGBConverter(workingSpace);
        }
        
        public RGBColor ToRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion
            var converter = GetXYZToRGBConverter(TargetRGBWorkingSpace);
            RGBColor result = converter.Convert(adapted);
            return result;
        }

        public RGBColor ToRGB(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }

        public RGBColor ToRGB(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }
    }
}