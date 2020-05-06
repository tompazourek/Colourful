

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LMSColor" /> and back.
    /// </summary>
    public sealed class XYZAndLMSConverter : IColorConversion<XYZColor, LMSColor>, IColorConversion<LMSColor, XYZColor>
    {
        /// <summary>
        /// Default transformation matrix used, when no other is set. (Bradford)
        /// <see cref="LMSTransformationMatrix" />
        /// </summary>
        public static readonly double[,] DefaultTransformationMatrix = LMSTransformationMatrix.Bradford;

        private double[,] _transformationMatrix;

        private double[,] _transformationMatrixInverse;

        /// <summary>
        /// Constructs with <see cref="DefaultTransformationMatrix" />
        /// </summary>
        public XYZAndLMSConverter() : this(DefaultTransformationMatrix)
        {
        }

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />), if not set <see cref="DefaultTransformationMatrix" /> will be used.</param>
        public XYZAndLMSConverter(double[,] transformationMatrix)
        {
            TransformationMatrix = transformationMatrix;
        }

        /// <summary>
        /// Transformation matrix used for the conversion (definition of the cone response domain).
        /// <see cref="LMSTransformationMatrix" />
        /// </summary>
        public double[,] TransformationMatrix
        {
            get => _transformationMatrix;
            internal set
            {
                _transformationMatrix = value;
                _transformationMatrixInverse = TransformationMatrix.Inverse();
            }
        }

        /// <summary>
        /// Converts from <see cref="LMSColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in LMSColor input)
        {
            var outputVector = _transformationMatrixInverse.MultiplyBy(input.Vector);
            var output = new XYZColor(outputVector);
            return output;
        }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="LMSColor" />.
        /// </summary>
        public LMSColor Convert(in XYZColor input)
        {
            var outputVector = TransformationMatrix.MultiplyBy(input.Vector);
            var output = new LMSColor(outputVector);
            return output;
        }
    }
}