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

#if (DYNAMIC)
/// <summary>
/// Convert to LMS color
/// </summary>
        public LMSColor ToLMS<T>(T color) where T : IColorVector
        {
            if (color is LMSColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLMS(source);
        }
#endif
    }
}