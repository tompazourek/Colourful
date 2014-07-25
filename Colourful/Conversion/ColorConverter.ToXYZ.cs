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
            if (color == null) throw new ArgumentNullException("color");

            // uncompanding
            var rgbConverter = new RGBToLinearRGBConverter();
            LinearRGBColor linear = rgbConverter.Convert(color);

            // conversion
            var result = ToXYZ(linear);
            return result;
        }

        public XYZColor ToXYZ(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException("color");
            // conversion
            var converterXyz = GetLinearRGBToXYZConverter(color.WorkingSpace);
            XYZColor unadapted = converterXyz.Convert(color);

            // adaptation
            XYZColor adapted = color.WorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WorkingSpace.WhitePoint);

            return adapted;
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public XYZColor ToXYZ(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new xyYAndXYZConverter();
            XYZColor converted = converter.Convert(color);
            return converted;
        }

        public XYZColor ToXYZ(LabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new LabToXYZConverter();
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to Lab
            var labConverter = new LChabToLabConverter();
            LabColor labColor = labConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            XYZColor result = ToXYZ(labColor);
            return result;
        }

        public XYZColor ToXYZ(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new HunterLabToXYZConverter();
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion
            var converter = new LuvToXYZConverter();
            XYZColor unadapted = converter.Convert(color);

            // adaptation
            XYZColor adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        public XYZColor ToXYZ(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException("color");

            // conversion to Luv
            var luvConverter = new LChuvToLuvConverter();
            LuvColor labColor = luvConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            XYZColor result = ToXYZ(labColor);
            return result;
        }
    }
}