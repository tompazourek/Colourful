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
using System.Linq;
using System.Text;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        public LMSColor ToLMS(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var result = converter.Convert(color);
            return result;
        }

        public LMSColor ToLMS(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        public LMSColor ToLMS(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

#if (DYNAMIC)
        public LMSColor ToLMS<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LMSColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLMS(source);
            }
        }
#endif
    }
}