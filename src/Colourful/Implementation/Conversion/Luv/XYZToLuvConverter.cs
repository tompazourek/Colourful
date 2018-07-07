using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LuvColor" />.
    /// </summary>
    public sealed class XYZToLuvConverter : IColorConversion<XYZColor, LuvColor>
    {
        /// <summary>
        /// Constructs with <see cref="LuvColor.DefaultWhitePoint" />
        /// </summary>
        public XYZToLuvConverter()
            : this(LuvColor.DefaultWhitePoint)
        {
        }

        /// <summary>
        /// Constructs with arbitrary white point
        /// </summary>
        public XYZToLuvConverter(XYZColor labWhitePoint)
        {
            LuvWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LuvColor.DefaultWhitePoint" /> is used.
        /// </summary>
        public XYZColor LuvWhitePoint { get; }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="LuvColor" />.
        /// </summary>
        public LuvColor Convert(in XYZColor input)
        {
            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Luv.html

            var yr = input.Y / LuvWhitePoint.Y;
            var up = Compute_up(input);
            var vp = Compute_vp(input);
            var upr = Compute_up(LuvWhitePoint);
            var vpr = Compute_vp(LuvWhitePoint);

            var L = yr > CIEConstants.Epsilon ? 116 * Math.Pow(yr, 1 / 3d) - 16 : CIEConstants.Kappa * yr;

            if (double.IsNaN(L) || L < 0)
                L = 0;

            var u = 13 * L * (up - upr);
            var v = 13 * L * (vp - vpr);

            if (double.IsNaN(u))
                u = 0;

            if (double.IsNaN(v))
                v = 0;

            return new LuvColor(L, u, v, LuvWhitePoint);
        }

        private static double Compute_up(XYZColor input) => 4 * input.X / (input.X + 15 * input.Y + 3 * input.Z);

        private static double Compute_vp(XYZColor input) => 9 * input.Y / (input.X + 15 * input.Y + 3 * input.Z);

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(XYZToLuvConverter other)
        {
            if (other == null)
                return false;

            return ReferenceEquals(this, other) || LuvWhitePoint.Equals(other.LuvWhitePoint);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is XYZToLuvConverter other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => LuvWhitePoint.GetHashCode();

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToLuvConverter left, XYZToLuvConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToLuvConverter left, XYZToLuvConverter right) => !Equals(left, right);

        #endregion
    }
}