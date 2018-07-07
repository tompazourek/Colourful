using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in XYZColor color)
        {
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
        public LabColor ToLab(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in LChabColor color)
        {
            // conversion (preserving white point)
            var converter = LChabToLabConverter.Default;
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
        public LabColor ToLab(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE L*a*b* (1976) color
        /// </summary>
        public LabColor ToLab(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLab(xyzColor);
            return result;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to CIE L*a*b* (1976) color
/// </summary>
        public LabColor ToLab<T>(T color) where T : struct, IColorVector
        {
            if (color is LabColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLab(source);
        }
#endif
    }
}