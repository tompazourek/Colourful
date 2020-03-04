using System;
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

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv<T>(T color) where T : IColorVector
        {
            switch (color)
            {
                case RGBColor typedColor:
                    return ToLChuv(in typedColor);
                case LinearRGBColor typedColor:
                    return ToLChuv(in typedColor);
                case XYZColor typedColor:
                    return ToLChuv(in typedColor);
                case xyYColor typedColor:
                    return ToLChuv(in typedColor);
                case HunterLabColor typedColor:
                    return ToLChuv(in typedColor);
                case LabColor typedColor:
                    return ToLChuv(in typedColor);
                case LChabColor typedColor:
                    return ToLChuv(in typedColor);
                case LuvColor typedColor:
                    return ToLChuv(in typedColor);
                case LChuvColor typedColor:
                    return typedColor;
                case LMSColor typedColor:
                    return ToLChuv(in typedColor);
                default:
                    throw new ArgumentException($"Cannot accept type '{typeof(T)}'.", nameof(color));
            }
        }
    }
}