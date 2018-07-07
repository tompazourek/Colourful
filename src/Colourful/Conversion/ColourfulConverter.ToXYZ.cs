using Colourful.Implementation.Conversion;

namespace Colourful.Conversion
{
    public partial class ColourfulConverter
    {
        private LinearRGBToXYZConverter _lastLinearRGBToXYZConverter;

        private LinearRGBToXYZConverter GetLinearRGBToXYZConverter(IRGBWorkingSpace workingSpace)
        {
            if (_lastLinearRGBToXYZConverter != null &&
                _lastLinearRGBToXYZConverter.SourceRGBWorkingSpace.Equals(workingSpace))
                return _lastLinearRGBToXYZConverter;

            return _lastLinearRGBToXYZConverter = new LinearRGBToXYZConverter(workingSpace);
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in RGBColor color)
        {
            // uncompanding
            var rgbConverter = RGBToLinearRGBConverter.Default;

            var linear = rgbConverter.Convert(color);

            // conversion
            var result = ToXYZ(linear);
            return result;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LinearRGBColor color)
        {
            // conversion
            var converterXyz = GetLinearRGBToXYZConverter(color.WorkingSpace);
            var unadapted = converterXyz.Convert(color);

            // adaptation
            var adapted = color.WorkingSpace.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WorkingSpace.WhitePoint);

            return adapted;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in xyYColor color)
        {
            // conversion
            var converter = xyYAndXYZConverter.Default;
            var converted = converter.Convert(color);
            return converted;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LabColor color)
        {
            // conversion
            var converter = LabToXYZConverter.Default;
            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LChabColor color)
        {
            // conversion to Lab
            var labConverter = LChabToLabConverter.Default;

            var labColor = labConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            var result = ToXYZ(labColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in HunterLabColor color)
        {
            // conversion
            var converter = HunterLabToXYZConverter.Default;

            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LuvColor color)
        {
            // conversion
            var converter = LuvToXYZConverter.Default;
            var unadapted = converter.Convert(color);

            // adaptation
            var adapted = color.WhitePoint.Equals(WhitePoint) || !IsChromaticAdaptationPerformed
                ? unadapted
                : Adapt(unadapted, color.WhitePoint);

            return adapted;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LChuvColor color)
        {
            // conversion to Luv
            var luvConverter = LChuvToLuvConverter.Default;

            var labColor = luvConverter.Convert(color);

            // conversion to XYZ (incl. adaptation)
            var result = ToXYZ(labColor);
            return result;
        }

        /// <summary>
        /// Convert to CIE 1931 XYZ color
        /// </summary>
        public XYZColor ToXYZ(in LMSColor color)
        {
            // conversion
            var converter = _cachedXYZAndLMSConverter;
            var converted = converter.Convert(color);
            return converted;
        }

#if (DYNAMIC)
/// <summary>
/// Convert to CIE 1931 XYZ color
/// </summary>
        public XYZColor ToXYZ<T>(T color) where T : IColorVector
        {
            if (color is XYZColor converted)
            {
                return converted;
            }

            dynamic source = color;

            return ToXYZ(source);
        }
#endif
    }
}