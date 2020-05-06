using System;


namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class LinearRGBToXYZConverter : LinearRGBAndXYZConverterBase, IColorConversion<LinearRGBColor, XYZColor>, IEquatable<LinearRGBToXYZConverter>
    {
        private readonly double[,] _conversionMatrix;

        /// <param name="sourceRGBWorkingSpace">Source RGB working space</param>
        public LinearRGBToXYZConverter(in IRGBWorkingSpace sourceRGBWorkingSpace)
        {
            SourceRGBWorkingSpace = sourceRGBWorkingSpace;
            _conversionMatrix = GetRGBToXYZMatrix(in sourceRGBWorkingSpace);
        }

        /// <summary>
        /// Source RGB working space
        /// </summary>
        public IRGBWorkingSpace SourceRGBWorkingSpace { get; }

        /// <summary>
        /// Converts from <see cref="LinearRGBColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in LinearRGBColor input)
        {
            if (!Equals(input.WorkingSpace, SourceRGBWorkingSpace))
                throw new InvalidOperationException("Working space of input RGB color must be equal to converter source RGB working space.");

            var xyz = _conversionMatrix.MultiplyBy(input.Vector);

            var converted = new XYZColor(xyz);
            return converted;
        }

        #region Equality

        /// <inheritdoc />
        public bool Equals(LinearRGBToXYZConverter other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(SourceRGBWorkingSpace, other.SourceRGBWorkingSpace);
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is LinearRGBToXYZConverter other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => (SourceRGBWorkingSpace != null ? SourceRGBWorkingSpace.GetHashCode() : 0);

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToXYZConverter left, LinearRGBToXYZConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToXYZConverter left, LinearRGBToXYZConverter right) => !Equals(left, right);

        #endregion
    }
}