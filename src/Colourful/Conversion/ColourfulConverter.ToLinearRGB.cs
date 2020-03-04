using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in RGBColor color)
        {
            var converter = RGBToLinearRGBConverter.Default;

            return converter.Convert(color);
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in XYZColor color)
        {
            // adaptation
            var adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion to linear RGB
            var xyzConverter = GetXYZToLinearRGBConverter(TargetRGBWorkingSpace);
            var result = xyzConverter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LinearRGBColor typedColor:
                    return typedColor;
                case XYZColor typedColor:
                    return ToLinearRGB(in typedColor);
                case xyYColor typedColor:
                    return ToLinearRGB(in typedColor);
                case HunterLabColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LabColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LChabColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LuvColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LChuvColor typedColor:
                    return ToLinearRGB(in typedColor);
                case LMSColor typedColor:
                    return ToLinearRGB(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}