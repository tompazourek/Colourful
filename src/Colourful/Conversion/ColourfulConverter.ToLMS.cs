using System;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in XYZColor color)
        {
            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToLMS(in typedColor);
                case LinearRGBColor typedColor:
                    return ToLMS(in typedColor);
                case XYZColor typedColor:
                    return ToLMS(in typedColor);
                case xyYColor typedColor:
                    return ToLMS(in typedColor);
                case HunterLabColor typedColor:
                    return ToLMS(in typedColor);
                case LabColor typedColor:
                    return ToLMS(in typedColor);
                case LChabColor typedColor:
                    return ToLMS(in typedColor);
                case LuvColor typedColor:
                    return ToLMS(in typedColor);
                case LChuvColor typedColor:
                    return ToLMS(in typedColor);
                case LMSColor typedColor:
                    return typedColor;
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}