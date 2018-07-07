using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in XYZColor color)
        {
            var luvColor = ToLuv(color);
            var result = ToLChuv(luvColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in LuvColor color)
        {
            // adaptation to target luv white point (LuvWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = LuvToLChuvConverter.Default;
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to CIE L*C*h° (Luv) color
/// </summary>
        public LChuvColor ToLChuv<T>(T color) where T : IColorVector
        {
            if (color is LChuvColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLChuv(source);
        }
#endif
    }
}