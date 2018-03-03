using System;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LinearRGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

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
        public HunterLabColor ToHunterLab(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToHunterLab(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to Hunter Lab color
        /// </summary>
        public HunterLabColor ToHunterLab<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as HunterLabColor;

            if (converted != null)
            {
                return converted;
            }

            dynamic source = color;

            return ToHunterLab(source);
        }
#endif
    }
}