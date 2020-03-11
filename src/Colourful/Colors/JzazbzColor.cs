using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// Jz az bz color space introduced in Safdar & al. (2017).
    /// See: https://www.osapublishing.org/oe/abstract.cfm?uri=oe-25-13-15131
    /// </summary>
    public readonly struct JzazbzColor : IColorVector
    {
        #region Constructor

        /// <param name="jz"></param>
        /// <param name="az"></param>
        /// <param name="bz"></param>
        public JzazbzColor(double jz, double az, double bz)
        {
            Jz = jz;
            this.az = az;
            this.bz = bz;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions</param>
        public JzazbzColor(Vector vector) : this(vector[0], vector[1], vector[2])
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
        /// a_z (redness-greenness)
        /// </summary>
        /// <remarks>
        /// Ranges usually between -1 and 1.
        /// </remarks>
        public double az { get; }

        /// <summary>
        /// b_z (yellowness-blueness)
        /// </summary>
        /// <remarks>
        /// Ranges usually between -1 and 1.
        /// </remarks>
        public double bz { get; }

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { Jz, az, bz };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(JzazbzColor other) =>
            Jz == other.Jz &&
            az == other.az &&
            bz == other.bz;

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is JzazbzColor other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Jz.GetHashCode();
                hashCode = (hashCode * 397) ^ az.GetHashCode();
                hashCode = (hashCode * 397) ^ bz.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(JzazbzColor left, JzazbzColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(JzazbzColor left, JzazbzColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Jzazbz [Jz={0:0.##}, az={1:0.##}, bz={2:0.##}]", Jz, az, bz);

        #endregion
    }
}