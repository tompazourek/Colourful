using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChabColor" /> to <see cref="LabColor" />.
    /// </summary>
    public sealed class LChabToLabConverter : IColorConversion<LChabColor, LabColor>
    {
        public static readonly LChabToLabConverter Default = new LChabToLabConverter();

        /// <summary>
        /// Converts from <see cref="LChabColor" /> to <see cref="LabColor" />.
        /// </summary>
        public LabColor Convert(LChabColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(hDegrees);

            var a = C * Math.Cos(hRadians);
            var b = C * Math.Sin(hRadians);

            var output = new LabColor(L, a, b, input.WhitePoint);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            return obj is LChabToLabConverter;
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LChabToLabConverter left, LChabToLabConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChabToLabConverter left, LChabToLabConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}