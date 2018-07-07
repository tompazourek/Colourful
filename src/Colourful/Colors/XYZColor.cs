using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// CIE 1931 XYZ color space
    /// </summary>
    public readonly struct XYZColor : IColorVector
    {
        #region Constructor

        /// <param name="x">X (usually from 0 to 1)</param>
        /// <param name="y">Y (usually from 0 to 1)</param>
        /// <param name="z">Z (usually from 0 to 1)</param>
        public XYZColor(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1)</param>
        public XYZColor(Vector vector)
            : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double X { get; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double Y { get; }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double Z { get; }

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public Vector Vector => new[] { X, Y, Z };

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(XYZColor other) =>
            X == other.X &&
            Y == other.Y &&
            Z == other.Z;

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is XYZColor other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZColor left, XYZColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZColor left, XYZColor right) => !Equals(left, right);

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "XYZ [X={0:0.##}, Y={1:0.##}, Z={2:0.##}]", X, Y, Z);

        #endregion
    }
}