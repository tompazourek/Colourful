using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class HunterLabToXYZConverter : XYZAndHunterLabConverterBase, IColorConversion<HunterLabColor, XYZColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly HunterLabToXYZConverter Default = new HunterLabToXYZConverter();

        /// <summary>
        /// Converts from <see cref="HunterLabColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in HunterLabColor input)
        {
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
        public override bool Equals(object obj) => obj is HunterLabToXYZConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(HunterLabToXYZConverter left, HunterLabToXYZConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(HunterLabToXYZConverter left, HunterLabToXYZConverter right) => !Equals(left, right);

        #endregion
    }
}