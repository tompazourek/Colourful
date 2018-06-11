using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChuvColor" /> to <see cref="LuvColor" />.
    /// </summary>
    public class LChuvToLuvConverter : IColorConversion<LChuvColor, LuvColor>
    {
        /// <summary>
        /// Converts from <see cref="LChuvColor" /> to <see cref="LuvColor" />.
        /// </summary>
        public LuvColor Convert(LChuvColor input)
        {
            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(hDegrees);

            var u = C * Math.Cos(hRadians);
            var v = C * Math.Sin(hRadians);

            var output = new LuvColor(L, u, v, input.WhitePoint);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return true;
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return 1;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LChuvToLuvConverter left, LChuvToLuvConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChuvToLuvConverter left, LChuvToLuvConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}