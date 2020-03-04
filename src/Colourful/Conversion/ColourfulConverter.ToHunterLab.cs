using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in XYZColor color)
        {
            // adaptation
            var adapted = !WhitePoint.Equals(TargetHunterLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetHunterLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToHunterLabConverter(TargetHunterLabWhitePoint);
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab<T>(T color) where T : struct, IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToHunterLab(in typedColor);
                case LinearRGBColor typedColor:
                    return ToHunterLab(in typedColor);
                case XYZColor typedColor:
                    return ToHunterLab(in typedColor);
                case xyYColor typedColor:
                    return ToHunterLab(in typedColor);
                case HunterLabColor typedColor:
                    return typedColor;
                case LabColor typedColor:
                    return ToHunterLab(in typedColor);
                case LChabColor typedColor:
                    return ToHunterLab(in typedColor);
                case LuvColor typedColor:
                    return ToHunterLab(in typedColor);
                case LChuvColor typedColor:
                    return ToHunterLab(in typedColor);
                case LMSColor typedColor:
                    return ToHunterLab(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}