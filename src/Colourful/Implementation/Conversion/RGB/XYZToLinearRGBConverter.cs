

using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LinearRGBColor" />.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace" /> when not set.
    /// </remarks>
    public sealed class XYZToLinearRGBConverter : LinearRGBAndXYZConverterBase, IColorConversion<XYZColor, LinearRGBColor>, IEquatable<XYZToLinearRGBConverter>
    {
        private readonly double[,] _conversionMatrix;

        /// <summary>
        /// Constructs with <see cref="RGBColor.DefaultWorkingSpace" />.
        /// </summary>
        public XYZToLinearRGBConverter() : this(null)
        {
        }

        /// <summary>
        /// Constructs with arbitrary working space.
        /// </summary>
        public XYZToLinearRGBConverter(in IRGBWorkingSpace targetRGBWorkingSpace)
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
            var result = new LinearRGBColor(in uncompandedVector, TargetRGBWorkingSpace);
            return result;
        }

        #region Equality

        /// <inheritdoc />
        public bool Equals(XYZToLinearRGBConverter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TargetRGBWorkingSpace, other.TargetRGBWorkingSpace);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is XYZToLinearRGBConverter other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => (TargetRGBWorkingSpace != null ? TargetRGBWorkingSpace.GetHashCode() : 0);

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right) => !Equals(left, right);

        #endregion
    }
}