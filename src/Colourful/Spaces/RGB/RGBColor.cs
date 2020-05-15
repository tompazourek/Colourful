using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Internals;

#if (DRAWING)
using System.Drawing;
#endif

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>.
    /// </summary>
    public readonly struct RGBColor : IColorSpace, IColorVector, IEquatable<RGBColor>
    {
        #region Constructor

        /// <param name="r">Red (from 0 to 1).</param>
        /// <param name="g">Green (from 0 to 1).</param>
        /// <param name="b">Blue (from 0 to 1).</param>
        public RGBColor(in double r, in double g, in double b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1).</param>
        public RGBColor(in double[] vector)
            : this(in vector[0], in vector[1], in vector[2])
        {
        }

#if (DRAWING)
        /// <param name="color"></param>
        public RGBColor(in Color color)
            : this(color.R / 255d, color.G / 255d, color.B / 255d)
        {
        }
#endif

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
        public double[] Vector => new[] { R, G, B };

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(RGBColor other) => R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is RGBColor other && Equals(other);

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
        public static bool operator ==(RGBColor left, RGBColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBColor left, RGBColor right) => !Equals(left, right);

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal.
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1).</param>
        public static RGBColor FromGrey(in double value) => new RGBColor(in value, in value, in value);

        /// <summary>
        /// Creates RGB color from 8-bit channels.
        /// </summary>
        public static RGBColor FromRGB8bit(in byte red, in byte green, in byte blue) => new RGBColor(red / 255d, green / 255d, blue / 255d);

        #endregion

#if (DRAWING)
        #region Color conversions

        /// <summary>
        /// Convert to <see cref="System.Drawing.Color" />.
        /// </summary>
        public Color ToColor() => this;

        /// <summary>
        /// Convert to <see cref="System.Drawing.Color" />.
        /// </summary>
        public static implicit operator Color(RGBColor input)
        {
            var r = (byte)Math.Round(input.R * 255, MidpointRounding.AwayFromZero).CropRange(0, 255);
            var g = (byte)Math.Round(input.G * 255, MidpointRounding.AwayFromZero).CropRange(0, 255);
            var b = (byte)Math.Round(input.B * 255, MidpointRounding.AwayFromZero).CropRange(0, 255);
            var output = Color.FromArgb(r, g, b);
            return output;
        }

        /// <summary>
        /// Convert from <see cref="System.Drawing.Color" />.
        /// </summary>
        public static explicit operator RGBColor(Color color) => new RGBColor(in color);

        #endregion
#endif

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "RGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);

        #endregion
    }
}