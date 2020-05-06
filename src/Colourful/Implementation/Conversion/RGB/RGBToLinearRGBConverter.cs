 

using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="RGBColor" /> to <see cref="LinearRGBColor" />.
    /// </summary>
    public sealed class RGBToLinearRGBConverter : IColorConversion<RGBColor, LinearRGBColor>, IEquatable<RGBToLinearRGBConverter>
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
            var uncompandedVector = UncompandVector(in input);
            var converted = new LinearRGBColor(in uncompandedVector, input.WorkingSpace);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding" />) to RGB vector.
        /// </summary>
        private static double[] UncompandVector(in RGBColor rgbColor)
        {
            var companding = rgbColor.WorkingSpace.Companding;
            var compandedVector = rgbColor.Vector;
            double[] uncompandedVector = 
            {
                companding.InverseCompanding(compandedVector[0]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[1]).CropRange(0, 1),
                companding.InverseCompanding(compandedVector[2]).CropRange(0, 1),
            };
            return uncompandedVector;
        }
        
        #region Equality
        
        /// <inheritdoc />
        public bool Equals(RGBToLinearRGBConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is RGBToLinearRGBConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right) => !Equals(left, right);

        #endregion
    }
}