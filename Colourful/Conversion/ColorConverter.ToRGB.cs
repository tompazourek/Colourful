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
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColorConverter
    {
        private XYZToLinearRGBConverter _lastXYZToLinearRGBConverter;

        private XYZToLinearRGBConverter GetXYZToLinearRGBConverter(IRGBWorkingSpace workingSpace)
        {
            if (_lastXYZToLinearRGBConverter != null &&
                _lastXYZToLinearRGBConverter.TargetRGBWorkingSpace.Equals(workingSpace))
                return _lastXYZToLinearRGBConverter;

            return _lastXYZToLinearRGBConverter = new XYZToLinearRGBConverter(workingSpace);
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public RGBColor ToRGB(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new LinearRGBToRGBConverter();
            RGBColor result = converter.Convert(color);
            return result;
        }
        
        public RGBColor ToRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var linear = ToLinearRGB(color);

            // companding to RGB
            RGBColor result = ToRGB(linear);
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

        public RGBColor ToRGB(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            RGBColor result = ToRGB(xyzColor);
            return result;
        }
    }
}