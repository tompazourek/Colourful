using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in RGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LinearRGBColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in XYZColor color)
        {
            // adaptation
            var adapted = !WhitePoint.Equals(TargetHunterLabWhitePoint) && IsChromaticAdaptationPerformed
                ? ChromaticAdaptation.Transform(color, WhitePoint, TargetHunterLabWhitePoint)
                : color;

            // conversion
            var converter = new XYZToHunterLabConverter(TargetHunterLabWhitePoint);
            var result = converter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(in LMSColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to Hunter Lab color
/// </summary>
        public HunterLabColor ToHunterLab<T>(T color) where T : struct, IColorVector
        {
            if (color is HunterLabColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToHunterLab(source);
        }
#endif
    }
}