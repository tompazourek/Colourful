using System;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to LMS color
        /// </summary>
        public LMSColor ToLMS(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLMS(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to LMS color
        /// </summary>
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