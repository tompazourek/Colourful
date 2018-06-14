using System;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class LinearRGBToXYZConverter : LinearRGBAndXYZConverterBase, IColorConversion<LinearRGBColor, XYZColor>
    {
        private readonly Matrix _conversionMatrix;

        /// <param name="sourceRGBWorkingSpace">Source RGB working space</param>
        public LinearRGBToXYZConverter(IRGBWorkingSpace sourceRGBWorkingSpace)
        {
            SourceRGBWorkingSpace = sourceRGBWorkingSpace;
            _conversionMatrix = GetRGBToXYZMatrix(SourceRGBWorkingSpace);
        }

        /// <summary>
        /// Source RGB working space
        /// </summary>
        public IRGBWorkingSpace SourceRGBWorkingSpace { get; }

        /// <summary>
        /// Converts from <see cref="LinearRGBColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(LinearRGBColor input)
        {
            if (!Equals(input.WorkingSpace, SourceRGBWorkingSpace))
                throw new InvalidOperationException("Working space of input RGB color must be equal to converter source RGB working space.");

            var xyz = _conversionMatrix.MultiplyBy(input.Vector);

            var converted = new XYZColor(xyz);
            return converted;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(LinearRGBToXYZConverter other)
        {
            if (ReferenceEquals(this, other)) return true;

            return object.Equals(SourceRGBWorkingSpace, other.SourceRGBWorkingSpace);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            return obj is LinearRGBToXYZConverter other && Equals(other);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return SourceRGBWorkingSpace?.GetHashCode() ?? 0;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToXYZConverter left, LinearRGBToXYZConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToXYZConverter left, LinearRGBToXYZConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}