using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LinearRGBColor" /> to <see cref="RGBColor" />.
    /// </summary>
    public sealed class LinearRGBToRGBConverter : IColorConversion<LinearRGBColor, RGBColor>, IEquatable<LinearRGBToRGBConverter>
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
        private static RGBColor CompandVector(in double[] uncompandedVector, in IRGBWorkingSpace workingSpace)
        {
            var companding = workingSpace.Companding;
            double[] compandedVector = 
            {
                companding.Companding(uncompandedVector[0]).CropRange(0, 1),
                companding.Companding(uncompandedVector[1]).CropRange(0, 1),
                companding.Companding(uncompandedVector[2]).CropRange(0, 1)
            };
            var result = new RGBColor(in compandedVector, in workingSpace);
            return result;
        }

        #region Equality
        
        /// <inheritdoc />
        public bool Equals(LinearRGBToRGBConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LinearRGBToRGBConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right) => !Equals(left, right);

        #endregion
    }
}