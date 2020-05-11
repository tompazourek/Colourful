namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LMSToXYZConversion : IColorConversion<LMSColor, XYZColor>
    {
        private readonly double[,] _transformationMatrixInverse;

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />)</param>
        public LMSToXYZConversion(in double[,] transformationMatrix)
        {
            _transformationMatrixInverse = MatrixUtils.Inverse(transformationMatrix);
        }

        /// <inheritdoc />
        public XYZColor Convert(in LMSColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            var targetVector = MatrixUtils.MultiplyBy(in _transformationMatrixInverse, in sourceVector);
            var targetColor = new XYZColor(in targetVector);
            return targetColor;
        }
    }
}