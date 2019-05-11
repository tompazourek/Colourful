using System.Globalization;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// Hunter Lab color
    /// </summary>
    public readonly struct HunterLabColor : IColorVector
    {
        /// <summary>
        /// C standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        public static readonly XYZColor DefaultWhitePoint = Illuminants.C;

        #region Constructor

        /// <param name="l">L (lightness) (from 0 to 100)</param>
        /// <param name="a">a (usually from -100 to 100)</param>
        /// <param name="b">b (usually from -100 to 100)</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint" /> as white point.</remarks>
        public HunterLabColor(double l, double a, double b) : this(l, a, b, DefaultWhitePoint)
        {
        }

        /// <param name="l">L (lightness) (from 0 to 100)</param>
        /// <param name="a">a (usually from -100 to 100)</param>
        /// <param name="b">b (usually from -100 to 100)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public HunterLabColor(double l, double a, double b, XYZColor whitePoint)
        {
            L = l;
            this.a = a;
            this.b = b;
            _whitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint" /> as white point.</remarks>
        public HunterLabColor(Vector vector) : this(vector, DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants" />)</param>
        public HunterLabColor(Vector vector, XYZColor whitePoint)
            : this(vector[0], vector[1], vector[2], whitePoint)
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public double L { get; }

        /// <summary>
        /// a
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate green while positive values indicate magenta.
        /// </remarks>
        public double a { get; }

        /// <summary>
        /// b
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate blue and positive values indicate yellow.
        /// </remarks>
        public double b { get; }

        /// <remarks>
        /// <see cref="Illuminants" />
        /// </remarks>
        public XYZColor WhitePoint => _whitePoint ?? DefaultWhitePoint;

        private readonly XYZColor? _whitePoint;

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { L, a, b };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(HunterLabColor other) => L.Equals(other.L) && a.Equals(other.a) && b.Equals(other.b);

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is HunterLabColor other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(HunterLabColor left, HunterLabColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(HunterLabColor left, HunterLabColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "HunterLab [L={0:0.##}, a={1:0.##}, b={2:0.##}]", L, a, b);

        #endregion
    }
}