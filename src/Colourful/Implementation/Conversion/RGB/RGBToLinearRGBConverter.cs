using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="RGBColor" /> to <see cref="LinearRGBColor" />.
    /// </summary>
    public sealed class RGBToLinearRGBConverter : IColorConversion<RGBColor, LinearRGBColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly RGBToLinearRGBConverter Default = new RGBToLinearRGBConverter();

        /// <summary>
        /// Converts from <see cref="RGBColor" /> to <see cref="LinearRGBColor" />.
        /// </summary>
        public LinearRGBColor Convert(in RGBColor input)
        {
            var uncompandedVector = UncompandVector(input);
            var converted = new LinearRGBColor(uncompandedVector, input.WorkingSpace);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding" />) to RGB vector.
        /// </summary>
        private static Vector UncompandVector(in RGBColor rgbColor)
        {
            var companding = rgbColor.WorkingSpace.Companding;
            var compandedVector = rgbColor.Vector;
            Vector uncompandedVector = new[]
            {
                companding.InverseCompanding(compandedVector[0]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[1]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[2]).CropRange(0, 1)
            };
            return uncompandedVector;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(RGBToLinearRGBConverter other) => other != null;

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is RGBToLinearRGBConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right) => !Equals(left, right);

        #endregion
    }
}