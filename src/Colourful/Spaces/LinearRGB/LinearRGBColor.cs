using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>, which has linear channels (not companded).
    /// </summary>
    [SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
    public readonly struct LinearRGBColor : IColorSpace, IColorVector, IEquatable<LinearRGBColor>
    {
        #region Constructor

        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        public LinearRGBColor(in double r, in double g, in double b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1).</param>
        public LinearRGBColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// Red.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double R;

        /// <summary>
        /// Green.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double G;

        /// <summary>
        /// Blue.
        /// Ranges usually from 0 to 1.
        /// </summary>
        public readonly double B;

        /// <inheritdoc />
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
        public double[] Vector => new[] { R, G, B };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LinearRGBColor other) => R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LinearRGBColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(LinearRGBColor left, LinearRGBColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD10
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(LinearRGBColor left, LinearRGBColor right) => !Equals(left, right);

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal.
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1).</param>
        public static LinearRGBColor FromGrey(in double value) => new LinearRGBColor(in value, in value, in value);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LinearRGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);

        #endregion
    }
}