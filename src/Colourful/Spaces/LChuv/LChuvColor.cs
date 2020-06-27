using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE L*C*h°, cylindrical form of <see cref="LuvColor">CIE L*u*v* (1976)</see>.
    /// </summary>
    public readonly struct LChuvColor : IColorSpace, IColorVector, IEquatable<LChuvColor>
    {
        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100).</param>
        /// <param name="c">C* (chroma) (from 0 to 100).</param>
        /// <param name="h">h° (hue in degrees) (from 0 to 360).</param>
        public LChuvColor(in double l, in double c, in double h)
        {
            L = l;
            C = c;
            this.h = h;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
        public LChuvColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness).
        /// Ranges usually from 0 to 100.
        /// </summary>
        public readonly double L;

        /// <summary>
        /// C* (chroma).
        /// Ranges usually from 0 to 100.
        /// </summary>
        public readonly double C;

        /// <summary>
        /// h° (hue in degrees).
        /// Ranges usually from 0 to 360.
        /// </summary>
        public readonly double h;

        /// <inheritdoc />
        public double[] Vector => new[] { L, C, h };

        #endregion

        #region Saturation

        /// <summary>
        /// Computes saturation of the color (chroma normalized by lightness).
        /// Ranges from 0 to 100.
        /// </summary>
        public double Saturation => CylindricalFormulas.GetSaturation(in L, in C);

        /// <summary>
        /// Constructs the color using saturation instead of chromas.
        /// </summary>
        public static LChuvColor FromSaturation(in double lightness, in double hue, in double saturation)
        {
            var chroma = CylindricalFormulas.GetChroma(in saturation, in lightness);
            return new LChuvColor(in lightness, in chroma, in hue);
        }

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LChuvColor other) =>
            L == other.L &&
            C == other.C &&
            h == other.h;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LChuvColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ C.GetHashCode();
                hashCode = (hashCode * 397) ^ h.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        [ExcludeFromCodeCoverage]
        public static bool operator ==(LChuvColor left, LChuvColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        [ExcludeFromCodeCoverage]
        public static bool operator !=(LChuvColor left, LChuvColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LChuv [L={0:0.##}, C={1:0.##}, h={2:0.##}]", L, C, h);

        #endregion
    }
}