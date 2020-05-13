namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LinearRGBToXYZConverter : IColorConverter<LinearRGBColor, XYZColor>
    {
        private readonly double[,] _conversionMatrix;

        /// <param name="sourceWhitePoint">White point of the RGB working space.</param>
        /// <param name="sourcePrimaries">Chromaticity of RGB working space primaries.</param>
        public LinearRGBToXYZConverter(in XYZColor sourceWhitePoint, in RGBPrimaries sourcePrimaries)
        {
            _conversionMatrix = LinearRGBConversionUtils.GetRGBToXYZMatrix(in sourcePrimaries, in sourceWhitePoint);
        }

        /// <inheritdoc />
        public XYZColor Convert(in LinearRGBColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            var targetVector = MatrixUtils.MultiplyBy(in _conversionMatrix, in sourceVector);
            var targetColor = new XYZColor(in targetVector);
            return targetColor;
        }
    }
}