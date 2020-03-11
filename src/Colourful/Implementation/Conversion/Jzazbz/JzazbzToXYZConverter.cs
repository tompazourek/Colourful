using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="JzazbzColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class JzazbzToXYZConverter : IColorConversion<JzazbzColor, XYZColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly JzazbzToXYZConverter Default = new JzazbzToXYZConverter();

        /// <summary>
        /// White point of the resulting XYZ color.
        /// </summary>
        public static readonly XYZColor XYZWhitePoint = Illuminants.D65;

        /// <summary>
        /// Converts from <see cref="JzazbzColor" /> to <see cref="XYZColor" />.
        /// Resulting XYZ color is relative to D65 white point.
        /// </summary>
        public XYZColor Convert(in JzazbzColor input)
        {
            // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

            var Jz = input.Jz;
            var az = input.az;
            var bz = input.bz;

            Jz = Jz + 1.6295499532821566e-11;
            var Iz = Jz / (0.44 + 0.56 * Jz);
            var L = PerceptualQuantizerInverse(Iz + 1.386050432715393e-1 * az + 5.804731615611869e-2 * bz);
            var M = PerceptualQuantizerInverse(Iz - 1.386050432715393e-1 * az - 5.804731615611891e-2 * bz);
            var S = PerceptualQuantizerInverse(Iz - 9.601924202631895e-2 * az - 8.118918960560390e-1 * bz);

            var X = +1.661373055774069e+00 * L - 9.145230923250668e-01 * M + 2.313620767186147e-01 * S;
            var Y = -3.250758740427037e-01 * L + 1.571847038366936e+00 * M - 2.182538318672940e-01 * S;
            var Z = -9.098281098284756e-02 * L - 3.127282905230740e-01 * M + 1.522766561305260e+00 * S;

            var result = new XYZColor(X / 10000d, Y / 10000d, Z / 10000d);
            return result;
        }

        private static double PerceptualQuantizerInverse(double X)
        {
            var XX = Math.Pow(X, 7.460772656268214e-03);
            var result = 1e4 * Math.Pow((0.8359375 - XX) / (18.6875 * XX - 18.8515625), 6.277394636015326);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is JzazbzToXYZConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(JzazbzToXYZConverter left, JzazbzToXYZConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(JzazbzToXYZConverter left, JzazbzToXYZConverter right) => !Equals(left, right);

        #endregion
    }
}