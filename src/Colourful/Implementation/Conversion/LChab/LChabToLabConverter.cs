using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChabColor"/> to <see cref="LabColor"/>.
    /// </summary>
    public class LChabToLabConverter : IColorConversion<LChabColor, LabColor>
    {
        /// <summary>
        /// Converts from <see cref="LChabColor"/> to <see cref="LabColor"/>.
        /// </summary>
        public LabColor Convert(LChabColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(hDegrees);

            var a = C*Math.Cos(hRadians);
            var b = C*Math.Sin(hRadians);

            var output = new LabColor(L, a, b, input.WhitePoint);
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