namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class XYZToLMSConverter : IColorConverter<XYZColor, LMSColor>
    {
        private readonly double[,] _transformationMatrix;

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />)</param>
        public XYZToLMSConverter(in double[,] transformationMatrix)
        {
            _transformationMatrix = transformationMatrix;
        }

        /// <inheritdoc />
        public LMSColor Convert(in XYZColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            var targetVector = MatrixUtils.MultiplyBy(in _transformationMatrix, in sourceVector);
            var targetColor = new LMSColor(in targetVector);
            return targetColor;
        }
    }
}