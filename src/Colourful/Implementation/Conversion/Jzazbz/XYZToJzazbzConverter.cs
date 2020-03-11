using System;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="JzazbzColor" />.
    /// </summary>
    public sealed class XYZToJzazbzConverter : IColorConversion<XYZColor, JzazbzColor>
    {
        /// <summary>
        /// White point of the resulting XYZ color.
        /// </summary>
        public static readonly XYZColor XYZWhitePoint = Illuminants.D65;

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="JzazbzColor" />.
        /// Assuming that the XYZ color is relative to D65.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public JzazbzColor Convert(in XYZColor input)
        {
            // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

            var X = input.X * 10000d;
            var Y = input.Y * 10000d;
            var Z = input.Z * 10000d;

            var Lp = PerceptualQuantizer(0.674207838 * X + 0.382799340 * Y - 0.047570458 * Z);
            var Mp = PerceptualQuantizer(0.149284160 * X + 0.739628340 * Y + 0.083327300 * Z);
            var Sp = PerceptualQuantizer(0.070941080 * X + 0.174768000 * Y + 0.670970020 * Z);
            var Iz = 0.5 * (Lp + Mp);
            var az = 3.524000 * Lp - 4.066708 * Mp + 0.542708 * Sp;
            var bz = 0.199076 * Lp + 1.096799 * Mp - 1.295875 * Sp;
            var Jz = (0.44 * Iz) / (1 - 0.56 * Iz) - 1.6295499532821566e-11;

            var result = new JzazbzColor(Jz, az, bz);
            return result;
        }

        private static double PerceptualQuantizer(double x)
        {
            var xx = Math.Pow(x * 1e-4, 0.1593017578125);
            var result = Math.Pow((0.8359375 + 18.8515625 * xx) / (1 + 18.6875 * xx), 134.034375);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(XYZToJzazbzConverter other)
        {
            if (other == null)
                return false;

            return ReferenceEquals(this, other);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is XYZToJzazbzConverter other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 0;

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToJzazbzConverter left, XYZToJzazbzConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToJzazbzConverter left, XYZToJzazbzConverter right) => !Equals(left, right);

        #endregion
    }
}