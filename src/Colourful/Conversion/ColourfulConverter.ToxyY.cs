using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in XYZColor color)
        {
            // conversion
            var converter = new xyYAndXYZConverter();
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE xyY color
        /// </summary>
        public xyYColor ToxyY(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToxyY(xyzColor);
            return result;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to CIE xyY color
/// </summary>
        public xyYColor ToxyY<T>(T color) where T : IColorVector
        {
            if (color is xyYColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToxyY(source);
        }
#endif
    }
}