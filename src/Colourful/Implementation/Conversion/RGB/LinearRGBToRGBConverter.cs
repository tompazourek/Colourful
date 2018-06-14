using System;

using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
    /// </summary>
    public sealed class LinearRGBToRGBConverter : IColorConversion<LinearRGBColor, RGBColor>
    {
        /// <summary>
        /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
        /// </summary>
        public RGBColor Convert(LinearRGBColor input)
        {
            var result = CompandVector(input.Vector, input.WorkingSpace);
            return result;
        }

        /// <summary>
        /// Applying the working space companding function (<see cref="IRGBWorkingSpace.Companding" />) to uncompanded vector.
        /// </summary>
        private static RGBColor CompandVector(Vector uncompandedVector, IRGBWorkingSpace workingSpace)
        {
            var companding = workingSpace.Companding;
            Vector compandedVector = new[]
            {
                companding.Companding(uncompandedVector[0]).CropRange(0, 1),
                companding.Companding(uncompandedVector[1]).CropRange(0, 1),
                companding.Companding(uncompandedVector[2]).CropRange(0, 1)
            };
            var result = new RGBColor(compandedVector, workingSpace);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(LinearRGBToRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return true;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            return obj is LinearRGBToRGBConverter;
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}