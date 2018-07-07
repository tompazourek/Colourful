using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in XYZColor color)
        {
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
        public LuvColor ToLuv(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLuv(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*u*v* (1976) color
        /// </summary>
        public LuvColor ToLuv(in LChuvColor color)
        {
            // conversion (preserving white point)
            var converter = LChuvToLuvConverter.Default;
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
        public LuvColor ToLuv(in LMSColor color)
        {
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
            if (color is LuvColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLuv(source);
        }
#endif
    }
}