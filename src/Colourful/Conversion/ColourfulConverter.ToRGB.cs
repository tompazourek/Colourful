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
        public RGBColor ToRGB(in LinearRGBColor color)
        {
            // conversion
            var converter = LinearRGBToRGBConverter.Default;

            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in XYZColor color)
        {
            // conversion
            var linear = ToLinearRGB(color);

            // companding to RGB
            var result = ToRGB(linear);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to RGB color
        /// </summary>
        public RGBColor ToRGB<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return typedColor;
                case LinearRGBColor typedColor:
                    return ToRGB(in typedColor);
                case XYZColor typedColor:
                    return ToRGB(in typedColor);
                case xyYColor typedColor:
                    return ToRGB(in typedColor);
                case HunterLabColor typedColor:
                    return ToRGB(in typedColor);
                case LabColor typedColor:
                    return ToRGB(in typedColor);
                case LChabColor typedColor:
                    return ToRGB(in typedColor);
                case LuvColor typedColor:
                    return ToRGB(in typedColor);
                case LChuvColor typedColor:
                    return ToRGB(in typedColor);
                case LMSColor typedColor:
                    return ToRGB(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}