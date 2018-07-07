using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
    /// </summary>
    public sealed class LinearRGBToRGBConverter : IColorConversion<LinearRGBColor, RGBColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LinearRGBToRGBConverter Default = new LinearRGBToRGBConverter();

        /// <summary>
        /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
        /// </summary>
        public RGBColor Convert(in LinearRGBColor input)
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
        public bool Equals(LinearRGBToRGBConverter other) => other != null;

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LinearRGBToRGBConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right) => !Equals(left, right);

        #endregion
    }
}