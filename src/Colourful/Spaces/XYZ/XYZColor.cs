using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE 1931 XYZ color space.
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct XYZColor : IColorSpace, IColorVector, IEquatable<XYZColor>
    {
        #region Constructor

        /// <param name="x">X (usually from 0 to 1).</param>
        /// <param name="y">Y (usually from 0 to 1).</param>
        /// <param name="z">Z (usually from 0 to 1).</param>
        public XYZColor(in double x, in double y, in double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (usually from 0 to 1).</param>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
        public XYZColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// X.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double X;

        /// <summary>
        /// Y.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double Y;

        /// <summary>
        /// Z.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double Z;

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { X, Y, Z };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(XYZColor other) =>
            X == other.X &&
            Y == other.Y &&
            Z == other.Z;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is XYZColor other && Equals(other);

        /// <inheritdoc />
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
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(XYZColor left, XYZColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(XYZColor left, XYZColor right) => !Equals(left, right);

        #endregion

        #region Deconstructor

        /// <summary>
        /// Deconstructs color into individual channels.
        /// </summary>
        public void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "XYZ [X={0:0.##}, Y={1:0.##}, Z={2:0.##}]", X, Y, Z);

        #endregion
    }
}
