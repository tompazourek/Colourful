using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in XYZColor color)
        {
            var labColor = ToLab(color);
            var result = ToLChab(labColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in LabColor color)
        {
            // adaptation to target lab white point (LabWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = LabToLChabConverter.Default;
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to CIE L*C*h° (Lab) color
/// </summary>
        public LChabColor ToLChab<T>(T color) where T : struct, IColorVector
        {
            if (color is LChabColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLChab(source);
        }
#endif
    }
}