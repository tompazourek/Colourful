using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
    /// </summary>
    public sealed class LuvToLChuvConverter : IColorConversion<LuvColor, LChuvColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LuvToLChuvConverter Default = new LuvToLChuvConverter();

        /// <summary>
        /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
        /// </summary>
        public LChuvColor Convert(in LuvColor input)
        {
            double L = input.L, u = input.u, v = input.v;
            var C = Math.Sqrt(u * u + v * v);
            var hRadians = Math.Atan2(v, u);
            var hDegrees = Angle.NormalizeDegree(Angle.RadianToDegree(hRadians));

            var output = new LChuvColor(L, C, hDegrees, input.WhitePoint);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LuvToLChuvConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LuvToLChuvConverter left, LuvToLChuvConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LuvToLChuvConverter left, LuvToLChuvConverter right) => !Equals(left, right);

        #endregion
    }
}