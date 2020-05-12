namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class XYZToLinearRGBConversion : IColorConversion<XYZColor, LinearRGBColor>
    {
        private readonly double[,] _conversionMatrix;

        /// <param name="targetWhitePoint">White point of the RGB working space.</param>
        /// <param name="targetPrimaries">Chromaticity of RGB working space primaries.</param>
        public XYZToLinearRGBConversion(in XYZColor targetWhitePoint, in RGBPrimaries targetPrimaries)
        {
            _conversionMatrix = LinearRGBConversionUtils.GetXYZToRGBMatrix(in targetPrimaries, in targetWhitePoint);
        }

        /// <inheritdoc />
        public LinearRGBColor Convert(in XYZColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            var targetVector = MatrixUtils.MultiplyBy(in _conversionMatrix, in sourceVector);
            var targetColor = new LinearRGBColor(in targetVector);
            return targetColor;
        }
    }
}