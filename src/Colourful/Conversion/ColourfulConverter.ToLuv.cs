using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation
            var adapted = !WhitePoint.Equals(TargetLuvWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetLuvWhitePoint)
                : color;

            // conversion
            var converter = new XYZToLuvConverter(TargetLuvWhitePoint);
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion (preserving white point)
            var converter = new LChuvToLuvConverter();
            var unadapted = converter.Convert(color);

            if (!IsChromaticAdaptationPerformed)
                return unadapted;

            // adaptation to target luv white point (LuvWhitePoint)
            var adapted = Adapt(unadapted);
            return adapted;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LuvColor;

            if (converted != null)
            {
                return converted;
            }
            else
            {
                dynamic source = color;

                return ToLuv(source);
            }
        }
#endif
    }
}