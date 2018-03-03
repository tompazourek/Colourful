using System;
using System.Diagnostics.CodeAnalysis;
using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public LinearRGBColor ToLinearRGB(RGBColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // conversion
            var converter = new RGBToLinearRGBConverter();
            var result = converter.Convert(color);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(XYZColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            // adaptation
            var adapted = TargetRGBWorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? color
                : ChromaticAdaptation.Transform(color, WhitePoint, TargetRGBWorkingSpace.WhitePoint);

            // conversion to linear RGB
            var xyzConverter = GetXYZToLinearRGBConverter(TargetRGBWorkingSpace);
            var result = xyzConverter.Convert(adapted);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(xyYColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(LabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(LChabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(HunterLabColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(LuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(LChuvColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(LMSColor color)
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

#if (DYNAMIC)
        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB<T>(T color) where T : IColorVector
        {
            if (color == null) throw new ArgumentNullException(nameof(color));

            var converted = color as LinearRGBColor;

            if (converted != null)
            {
                return converted;
            }

            dynamic source = color;

            return ToLinearRGB(source);
        }
#endif
    }
}