

using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LMSColor" /> and back.
    /// </summary>
    public sealed class XYZAndLMSConverter : IColorConversion<XYZColor, LMSColor>, IColorConversion<LMSColor, XYZColor>, IEquatable<XYZAndLMSConverter>
    {
        /// <summary>
        /// Default transformation matrix used, when no other is set. (Bradford)
        /// <see cref="LMSTransformationMatrix" />
        /// </summary>
        public static readonly double[,] DefaultTransformationMatrix = LMSTransformationMatrix.Bradford;
        private readonly double[,] _transformationMatrixInverse;

        /// <summary>
        /// Constructs with <see cref="DefaultTransformationMatrix" />
        /// </summary>
        public XYZAndLMSConverter() : this(DefaultTransformationMatrix)
        {
        }

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />), if not set <see cref="DefaultTransformationMatrix" /> will be used.</param>
        public XYZAndLMSConverter(in double[,] transformationMatrix)
        {
            TransformationMatrix = transformationMatrix;
            _transformationMatrixInverse = transformationMatrix.Inverse();
        }

        /// <summary>
        /// Transformation matrix used for the conversion (definition of the cone response domain).
        /// <see cref="LMSTransformationMatrix" />
        /// </summary>
        public double[,] TransformationMatrix { get; }

        /// <summary>
        /// Converts from <see cref="LMSColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in LMSColor input)
        {
            var outputVector = _transformationMatrixInverse.MultiplyBy(input.Vector);
            var output = new XYZColor(in outputVector);
            return output;
        }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="LMSColor" />.
        /// </summary>
        public LMSColor Convert(in XYZColor input)
        {
            var outputVector = TransformationMatrix.MultiplyBy(input.Vector);
            var output = new LMSColor(in outputVector);
            return output;
        }

        #region Equality

        /// <inheritdoc />
        public bool Equals(XYZAndLMSConverter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TransformationMatrix, other.TransformationMatrix);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is XYZAndLMSConverter other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => (TransformationMatrix != null ? TransformationMatrix.GetHashCode() : 0);

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZAndLMSConverter left, XYZAndLMSConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZAndLMSConverter left, XYZAndLMSConverter right) => !Equals(left, right);

        #endregion
    }
}