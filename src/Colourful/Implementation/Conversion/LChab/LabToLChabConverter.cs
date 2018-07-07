using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor" /> to <see cref="LChabColor" />.
    /// </summary>
    public sealed class LabToLChabConverter : IColorConversion<LabColor, LChabColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LabToLChabConverter Default = new LabToLChabConverter();

        /// <summary>
        /// Converts from <see cref="LabColor" /> to <see cref="LChabColor" />.
        /// </summary>
        public LChabColor Convert(in LabColor input)
        {
            double L = input.L, a = input.a, b = input.b;
            var C = Math.Sqrt(a * a + b * b);
            var hRadians = Math.Atan2(b, a);
            var hDegrees = Angle.NormalizeDegree(Angle.RadianToDegree(hRadians));

            var output = new LChabColor(L, C, hDegrees, input.WhitePoint);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LabToLChabConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LabToLChabConverter left, LabToLChabConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LabToLChabConverter left, LabToLChabConverter right) => !Equals(left, right);

        #endregion
    }
}