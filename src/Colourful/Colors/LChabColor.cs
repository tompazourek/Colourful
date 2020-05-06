using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE L*C*h°, cylindrical form of <see cref="LabColor">CIE L*a*b* (1976)</see>
    /// </summary>
    public readonly struct LChabColor : IColorVector, IEquatable<LChabColor>
    {
        /// <summary>
        /// D50 standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D50;

        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="c">C* (chroma) (from 0 to 100)</param>
        /// <param name="h">h° (hue in degrees) (from 0 to 360)</param>
        public LChabColor(in double l, in double c, in double h) : this(in l, in c, in h, in DefaultWhitePoint)
        {
        }

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="c">C* (chroma) (from 0 to 100)</param>
        /// <param name="h">h° (hue in degrees) (from 0 to 360)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public LChabColor(in double l, in double c, in double h, in XYZColor whitePoint)
        {
            L = l;
            C = c;
            this.h = h;
            _whitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint" /> as white point.</remarks>
        public LChabColor(in double[] vector) : this(in vector, in DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public LChabColor(in double[] vector, in XYZColor whitePoint)
            : this(in vector[0], in vector[1], in vector[2], in whitePoint)
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public readonly double L;

        /// <summary>
        /// C* (chroma)
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 100.
        /// </remarks>
        public readonly double C;

        /// <summary>
        /// h° (hue in degrees)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 360.
        /// </remarks>
        public readonly double h;

        /// <remarks>
        /// <see cref="Illuminants" />
        /// </remarks>
        public XYZColor WhitePoint => _whitePoint ?? DefaultWhitePoint;

        private readonly XYZColor? _whitePoint;

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public double[] Vector => new[] { L, C, h };

        #endregion

        #region Saturation

        /// <summary>
        /// Computes saturation of the color (chroma normalized by lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public double Saturation => SaturationLChFormulas.GetSaturation(in L, in C);
        
        /// <summary>
        /// Constructs the color using saturation instead of chromas
        /// </summary>
        public static LChabColor FromSaturation(in double lightness, in double hue, in double saturation)
        {
            var chroma = SaturationLChFormulas.GetChroma(in saturation, in lightness);
            return new LChabColor(in lightness, in chroma, in hue);
        }

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LChabColor other) =>
            L == other.L &&
            C == other.C &&
            h == other.h;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LChabColor other && Equals(other);

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
        public static bool operator ==(LChabColor left, LChabColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChabColor left, LChabColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LChab [L={0:0.##}, C={1:0.##}, h={2:0.##}]", L, C, h);

        #endregion
    }
}