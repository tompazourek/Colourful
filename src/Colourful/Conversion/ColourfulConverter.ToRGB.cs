using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        private XYZToLinearRGBConverter _lastXYZToLinearRGBConverter;

        private XYZToLinearRGBConverter GetXYZToLinearRGBConverter(IRGBWorkingSpace workingSpace)
        {
            if (_lastXYZToLinearRGBConverter != null &&
                _lastXYZToLinearRGBConverter.TargetRGBWorkingSpace.Equals(workingSpace))
                return _lastXYZToLinearRGBConverter;

            return _lastXYZToLinearRGBConverter = new XYZToLinearRGBConverter(workingSpace);
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new LinearRGBToRGBConverter();
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var linear = ToLinearRGB(color);

            // companding to RGB
            var result = ToRGB(linear);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as RGBColor;

            if (converted != null)
            {
                return converted;
            }

            dynamic source = color;

            return ToRGB(source);
        }
#endif
    }
}