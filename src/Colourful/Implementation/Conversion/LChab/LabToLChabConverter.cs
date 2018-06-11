using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor" /> to <see cref="LChabColor" />.
    /// </summary>
    public class LabToLChabConverter : IColorConversion<LabColor, LChabColor>
    {
        /// <summary>
        /// Converts from <see cref="LabColor" /> to <see cref="LChabColor" />.
        /// </summary>
        public LChabColor Convert(LabColor input)
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
        public static bool operator ==(LabToLChabConverter left, LabToLChabConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LabToLChabConverter left, LabToLChabConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}