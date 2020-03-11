using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// Jz Cz hz, cylindrical form of <see cref="JzazbzColor">Jzazbz</see>.
    /// </summary>
    public readonly struct JzCzhzColor : IColorVector
    {
        #region Constructor

        /// <param name="jz"></param>
        /// <param name="cz"></param>
        /// <param name="hz"></param>
        public JzCzhzColor(in double jz, in double cz, in double hz)
        {
            Jz = jz;
            Cz = cz;
            this.hz = hz;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        public JzCzhzColor(Vector vector) : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// J_z (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges usually between 0 and 1.
        /// </remarks>
        public double Jz { get; }

        /// <summary>
        /// C_z (chroma)
        /// </summary>
        /// <remarks>
        /// Ranges usually between 0 and 1.
        /// </remarks>
        public double Cz { get; }

        /// <summary>
        /// h_z (hue in degrees)
        /// </summary>
        /// <remarks>
        /// Ranges usually between 0 and 360.
        /// </remarks>
        public double hz { get; }

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { Jz, Cz, hz };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(JzCzhzColor other) =>
            Jz == other.Jz &&
            Cz == other.Cz &&
            hz == other.hz;

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is JzCzhzColor other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Jz.GetHashCode();
                hashCode = (hashCode * 397) ^ Cz.GetHashCode();
                hashCode = (hashCode * 397) ^ hz.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(JzCzhzColor left, JzCzhzColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(JzCzhzColor left, JzCzhzColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "JzCzhz [Jz={0:0.##}, Cz={1:0.##}, hz={2:0.##}]", Jz, Cz, hz);

        #endregion
    }
}