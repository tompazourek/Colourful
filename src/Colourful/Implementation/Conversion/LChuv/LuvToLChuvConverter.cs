using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
    /// </summary>
    public sealed class LuvToLChuvConverter : IColorConversion<LuvColor, LChuvColor>
    {
        /// <summary>
        /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
        /// </summary>
        public LChuvColor Convert(LuvColor input)
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
        public override bool Equals(object obj)
        {
            return obj is LuvToLChuvConverter;
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return 1;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LuvToLChuvConverter left, LuvToLChuvConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LuvToLChuvConverter left, LuvToLChuvConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}