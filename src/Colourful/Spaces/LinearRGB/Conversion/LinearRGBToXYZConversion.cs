namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LinearRGBToXYZConversion : IColorConversion<LinearRGBColor, XYZColor>
    {
        private readonly double[,] _conversionMatrix;

        /// <param name="sourcePrimaries">Chromaticity of RGB working space primaries.</param>
        /// <param name="sourceWhitePoint">White point of the RGB working space.</param>
        public LinearRGBToXYZConversion(in RGBPrimaries sourcePrimaries, in XYZColor sourceWhitePoint)
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