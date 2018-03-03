using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful
{
    /// <summary>
    /// CIE L*C*h°, cylindrical form of <see cref="LabColor">CIE L*a*b* (1976)</see>
    /// </summary>
    public class LChabColor : IColorVector
    {
        /// <summary>
        /// D50 standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D50;

        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="c">C* (chroma) (from 0 to 100)</param>
        /// <param name="h">h° (hue in degrees) (from 0 to 360)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LChabColor(double l, double c, double h) : this(l, c, h, DefaultWhitePoint)
        {
        }

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="c">C* (chroma) (from 0 to 100)</param>
        /// <param name="h">h° (hue in degrees) (from 0 to 360)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LChabColor(double l, double c, double h, XYZColor whitePoint)
        {
            L = l;
            C = c;
            this.h = h;
            WhitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        public LChabColor(Vector vector) : this(vector, DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public LChabColor(Vector vector, XYZColor whitePoint)
            : this(vector[0], vector[1], vector[2], whitePoint)
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L")]
        public double L { get; }

        /// <summary>
        /// C* (chroma)
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        public double C { get; }

        /// <summary>
        /// h° (hue in degrees)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 360.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "h"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "h")]
        public double h { get; }

        /// <remarks><see cref="Illuminants"/></remarks>
        public XYZColor WhitePoint { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector => new[] { L, C, h };

        #endregion

        #region Saturation

        /// <summary>
        /// Computes saturation of the color (chroma normalized by lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        public double Saturation => SaturationLChFormulas.GetSaturation(L, C);

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(LChabColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return L.Equals(other.L) && C.Equals(other.C) && h.Equals(other.h);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LChabColor)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode*397) ^ C.GetHashCode();
                hashCode = (hashCode*397) ^ h.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LChabColor left, LChabColor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChabColor left, LChabColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "LChab [L={0:0.##}, C={1:0.##}, h={2:0.##}]", L, C, h);
        }

        #endregion
    }
}