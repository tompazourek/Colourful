namespace Colourful.Adaptation
{
    /// <summary>
    /// Basic implementation of the von Kries chromatic adaptation model
    /// </summary>
    /// <remarks>
    /// Transformation described here:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class VonKriesChromaticAdaptation : IColorConverter<LMSColor, LMSColor>
    {
        private readonly double[,] _diagonalMatrix;

        /// <summary>
        /// Constructs a chromatic adaptation operation.
        /// </summary>
        /// <param name="sourceWhitePoint">Source white point</param>
        /// <param name="targetWhitePoint">Target white point</param>
        public VonKriesChromaticAdaptation(in LMSColor sourceWhitePoint, in LMSColor targetWhitePoint)
        {
            _diagonalMatrix = MatrixUtils.CreateDiagonal(targetWhitePoint.L / sourceWhitePoint.L, targetWhitePoint.M / sourceWhitePoint.M, targetWhitePoint.S / sourceWhitePoint.S);
        }

        /// <inheritdoc />
        public LMSColor Convert(in LMSColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            var targetVector = MatrixUtils.MultiplyBy(in _diagonalMatrix, in sourceVector);
            var targetColor = new LMSColor(in targetVector);
            return targetColor;
        }
    }
}