using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChabColor" /> to <see cref="LabColor" />.
    /// </summary>
    public sealed class LChabToLabConverter : IColorConversion<LChabColor, LabColor>, IEquatable<LChabToLabConverter>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LChabToLabConverter Default = new LChabToLabConverter();

        /// <summary>
        /// Converts from <see cref="LChabColor" /> to <see cref="LabColor" />.
        /// </summary>
        public LabColor Convert(in LChabColor input)
        {
            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(in hDegrees);

            var a = C * Math.Cos(hRadians);
            var b = C * Math.Sin(hRadians);

            var output = new LabColor(in L, in a, in b, input.WhitePoint);
            return output;
        }
        
        #region Equality
        
        /// <inheritdoc />
        public bool Equals(LChabToLabConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LChabToLabConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LChabToLabConverter left, LChabToLabConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChabToLabConverter left, LChabToLabConverter right) => !Equals(left, right);

        #endregion
    }
}