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
    public partial class ColourfulConverter
    {
        public LabColor ToLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // adaptation
            XYZColor adapted = !WhitePoint.Equals(TargetLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLabConverter(TargetLabWhitePoint);
            LabColor result = converter.Convert(adapted);
            return result;
        }

        public LabColor ToLab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion (perserving white point)
            var converter = new LChabToLabConverter();
            LabColor unadapted = converter.Convert(color);

            if (!IsChromaticAdaptationPerformed)
                return unadapted;

            // adaptation to target lab white point (LabWhitePoint)
            LabColor adapted = Adapt(unadapted);
            return adapted;
        }

        public LabColor ToLab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            XYZColor xyzColor = ToXYZ(color);
            LabColor result = ToLab(xyzColor);
            return result;
        }

        public LabColor ToLab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException("color");

            LabColor converted = color as LabColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLab(source);
            }
        }
    }
}