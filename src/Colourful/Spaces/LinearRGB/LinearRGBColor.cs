using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Internals;
using static System.Math;

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
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
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
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator ==(LinearRGBColor left, LinearRGBColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
        [ExcludeFromCodeCoverage]
#endif
        public static bool operator !=(LinearRGBColor left, LinearRGBColor right) => !Equals(left, right);

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal.
        /// </summary>
        /// <param name="value">Gray value (from 0 to 1).</param>
        public static LinearRGBColor FromGray(in double value) => new LinearRGBColor(in value, in value, in value);

        #endregion

        #region Deconstructor

        /// <summary>
        /// Deconstructs color into individual channels.
        /// </summary>
        public void Deconstruct(out double r, out double g, out double b)
        {
            r = R;
            g = G;
            b = B;
        }

        #endregion

        #region Utils

        /// <summary>
        /// Returns a new color that has each channel clamped. If the channel was lower than 0, it'll be 0. If it was higher than 1, it'll be 1.
        /// </summary>
        public LinearRGBColor Clamp()
        {
            var r = R.Clamp(0, 1);
            var g = G.Clamp(0, 1);
            var b = B.Clamp(0, 1);
            return new LinearRGBColor(r, g, b);
        }

        /// <summary>
        /// Returns a new color that has channels within the range, and at least one channel is maxed out to 1.
        /// Does this by dividing all channels by the maximum channel value out of those three.
        /// This is useful for scenarios where we're working with chromaticity.
        /// </summary>
        public LinearRGBColor NormalizeIntensity()
        {
            var maxChannel = Max(R, Max(G, B));
            if (maxChannel == 0)
            {
                maxChannel = 1;
            }

            var r = (R / maxChannel).Clamp(0, 1);
            var g = (G / maxChannel).Clamp(0, 1);
            var b = (B / maxChannel).Clamp(0, 1);
            return new LinearRGBColor(r, g, b);
        }

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LinearRGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);

        #endregion
    }
}
