using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// Jz Cz hz, cylindrical form of <see cref="JzazbzColor">Jzazbz</see>.
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct JzCzhzColor : IColorSpace, IColorVector, IEquatable<JzCzhzColor>
    {
        #region Constructor

        /// <param name="jz">J_z (lightness) (from 0 to 1).</param>
        /// <param name="cz">C_z (chroma) (from 0 and 1).</param>
        /// <param name="hz">h_z (hue in degrees) (from 0 to 360).</param>
        public JzCzhzColor(in double jz, in double cz, in double hz)
        {
            Jz = jz;
            Cz = cz;
            this.hz = hz;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
        public JzCzhzColor(in double[] vector) : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// J_z (lightness).
        /// Ranges usually between 0 and 1.
        /// </summary>
        public readonly double Jz;

        /// <summary>
        /// C_z (chroma).
        /// Ranges usually between 0 and 1.
        /// </summary>
        public readonly double Cz;

        /// <summary>
        /// h_z (hue in degrees).
        /// Ranges usually between 0 and 360.
        /// </summary>
        public readonly double hz;

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { Jz, Cz, hz };

        #endregion

        #region Saturation

        /// <summary>
        /// Computes saturation of the color (chroma normalized by lightness).
        /// Ranges from 0 to 100.
        /// </summary>
        public double Saturation => CylindricalFormulas.GetSaturation(in Jz, in Cz);

        /// <summary>
        /// Constructs the color using saturation instead of chromas.
        /// </summary>
        public static JzCzhzColor FromSaturation(in double lightness, in double hue, in double saturation)
        {
            var chroma = CylindricalFormulas.GetChroma(in saturation, in lightness);
            return new JzCzhzColor(in lightness, in chroma, in hue);
        }

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
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(JzCzhzColor left, JzCzhzColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(JzCzhzColor left, JzCzhzColor right) => !Equals(left, right);

        #endregion
        
        #region Deconstructor

        /// <summary>
        /// Deconstructs color into individual channels.
        /// </summary>
        public void Deconstruct(out double jz, out double cz, out double hz)
        {
            jz = Jz;
            cz = Cz;
            hz = this.hz;
        }

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "JzCzhz [Jz={0:0.##}, Cz={1:0.##}, hz={2:0.##}]", Jz, Cz, hz);

        #endregion
    }
}