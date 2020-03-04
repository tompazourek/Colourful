using System;
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

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab<T>(T color) where T : struct, IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToLChab(in typedColor);
                case LinearRGBColor typedColor:
                    return ToLChab(in typedColor);
                case XYZColor typedColor:
                    return ToLChab(in typedColor);
                case xyYColor typedColor:
                    return ToLChab(in typedColor);
                case HunterLabColor typedColor:
                    return ToLChab(in typedColor);
                case LabColor typedColor:
                    return ToLChab(in typedColor);
                case LChabColor typedColor:
                    return typedColor;
                case LuvColor typedColor:
                    return ToLChab(in typedColor);
                case LChuvColor typedColor:
                    return ToLChab(in typedColor);
                case LMSColor typedColor:
                    return ToLChab(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}