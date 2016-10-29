#region License

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
        private LinearRGBToXYZConverter _lastLinearRGBToXYZConverter;

        private LinearRGBToXYZConverter GetLinearRGBToXYZConverter(IRGBWorkingSpace workingSpace)
        {
            if (_lastLinearRGBToXYZConverter != null &&
                _lastLinearRGBToXYZConverter.SourceRGBWorkingSpace.Equals(workingSpace))
                return _lastLinearRGBToXYZConverter;

            return _lastLinearRGBToXYZConverter = new LinearRGBToXYZConverter(workingSpace);
        }

        public XYZColor ToXYZ(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // uncompanding
            var rgbConverter = new RGBToLinearRGBConverter();
            var linear = rgbConverter.Convert(color);

            // conversion
            var result = ToXYZ(linear);
            return result;
        }

        public XYZColor ToXYZ(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));
            // conversion
            var converterXyz = GetLinearRGBToXYZConverter(color.WorkingSpace);
            var unadapted = converterXyz.Convert(color);

            // adaptation
            var adapted = color.WorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WorkingSpace.WhitePoint);

            return adapted;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public XYZColor ToXYZ(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new xyYAndXYZConverter();
            var converted = converter.Convert(color);
            return converted;
        }

        public XYZColor ToXYZ(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new LabToXYZConverter();
            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion to Lab
            var labConverter = new LChabToLabConverter();
            var labColor = labConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            var result = ToXYZ(labColor);
            return result;
        }

        public XYZColor ToXYZ(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new HunterLabToXYZConverter();
            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new LuvToXYZConverter();
            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion to Luv
            var luvConverter = new LChuvToLuvConverter();
            var labColor = luvConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            var result = ToXYZ(labColor);
            return result;
        }

        public XYZColor ToXYZ(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var converted = converter.Convert(color);
            return converted;
        }

#if (DYNAMIC)
        public XYZColor ToXYZ<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as XYZColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToXYZ(source);
            }
        }
#endif
    }
}