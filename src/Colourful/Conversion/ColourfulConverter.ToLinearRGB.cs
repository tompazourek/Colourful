using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in RGBColor color)
        {
            var converter = RGBToLinearRGBConverter.Default;

            return converter.Convert(color);
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in XYZColor color)
        {
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
        public LinearRGBColor ToLinearRGB(in xyYColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LChabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in HunterLabColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LChuvColor color)
        {
            var xyzColor = ToXYZ(color);
            var result = ToLinearRGB(xyzColor);
            return result;
        }

        /// <summary>
        /// Convert to linear RGB
        /// </summary>
        public LinearRGBColor ToLinearRGB(in LMSColor color)
        {
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
            if (color is LinearRGBColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToLinearRGB(source);
        }
#endif
    }
}