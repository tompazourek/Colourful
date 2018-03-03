using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var labColor = ToLab(color);
            var result = ToLChab(labColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation to target lab white point (LabWhitePoint)
            var adapted = IsChromaticAdaptationPerformed ? Adapt(color) : color;

            // conversion (preserving white point)
            var converter = new LabToLChabConverter();
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLChab(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to CIE L*C*h° (Lab) color
        /// </summary>
        public LChabColor ToLChab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LChabColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLChab(source);
            }
        }
#endif
    }
}