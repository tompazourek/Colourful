using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation
            var adapted = !WhitePoint.Equals(TargetLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLabConverter(TargetLabWhitePoint);
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion (preserving white point)
            var converter = new LChabToLabConverter();
            var unadapted = converter.Convert(color);

            if (!IsChromaticAdaptationPerformed)
                return unadapted;

            // adaptation to target lab white point (LabWhitePoint)
            var adapted = Adapt(unadapted);
            return adapted;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LabColor;

            if (converted != null)
            {
                return converted;
            }

            dynamic source = color;

            return ToLab(source);
        }
#endif
    }
}