using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public class HunterLabToXYZConverter : XYZAndHunterLabConverterBase, IColorConversion<HunterLabColor, XYZColor>
    {
        /// <summary>
        /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(HunterLabColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            double L = input.L, a = input.a, b = input.b;
            double Xn = input.WhitePoint.X, Yn = input.WhitePoint.Y, Zn = input.WhitePoint.Z;

            var Ka = ComputeKa(input.WhitePoint);
            var Kb = ComputeKb(input.WhitePoint);

            var Y = MathUtils.Pow2(L / 100d) * Yn;
            var X = (a / Ka * Math.Sqrt(Y / Yn) + Y / Yn) * Xn;
            var Z = (b / Kb * Math.Sqrt(Y / Yn) - Y / Yn) * -Zn;

            var result = new XYZColor(X, Y, Z);
            return result;
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
        public static bool operator ==(HunterLabToXYZConverter left, HunterLabToXYZConverter right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(HunterLabToXYZConverter left, HunterLabToXYZConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}