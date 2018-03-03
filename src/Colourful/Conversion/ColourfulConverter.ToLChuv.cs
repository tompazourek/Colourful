using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var luvColor = ToLuv(color);
            var result = ToLChuv(luvColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation to target luv white point (LuvWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = new LuvToLChuvConverter();
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Luv) color
        /// </summary>
        public LChuvColor ToLChuv(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

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
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LChuvColor;

            if (converted != null)
            {
                return converted;
            }

            dynamic source = color;

            return ToLChuv(source);
        }
#endif
    }
}