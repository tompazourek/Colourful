using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LinearRGBColor" />.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace" /> when not set.
    /// </remarks>
    public sealed class XYZToLinearRGBConverter : LinearRGBAndXYZConverterBase, IColorConversion<XYZColor, LinearRGBColor>
    {
        private readonly Matrix _conversionMatrix;

        /// <summary>
        /// Constructs with <see cref="RGBColor.DefaultWorkingSpace" />.
        /// </summary>
        public XYZToLinearRGBConverter() : this(null)
        {
        }

        /// <summary>
        /// Constructs with arbitrary working space.
        /// </summary>
        public XYZToLinearRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace)
        {
            TargetRGBWorkingSpace = targetRGBWorkingSpace ?? RGBColor.DefaultWorkingSpace;
            _conversionMatrix = GetXYZToRGBMatrix(TargetRGBWorkingSpace);
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace" />.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="LinearRGBColor" />.
        /// </summary>
        public LinearRGBColor Convert(in XYZColor input)
        {
            var inputVector = input.Vector;
            var uncompandedVector = _conversionMatrix.MultiplyBy(inputVector).CropRange(0, 1);
            var result = new LinearRGBColor(uncompandedVector, TargetRGBWorkingSpace);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(XYZToLinearRGBConverter other)
        {
            if (other == null)
                return false;

            return ReferenceEquals(this, other) || TargetRGBWorkingSpace.Equals(other.TargetRGBWorkingSpace);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is XYZToLinearRGBConverter other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => TargetRGBWorkingSpace?.GetHashCode() ?? 0;

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right) => !Equals(left, right);

        #endregion
    }
}