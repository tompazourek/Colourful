using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Implementation;
#if (DRAWING)
using System.Drawing;

#endif

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>
    /// </summary>
    public readonly struct RGBColor : IColorVector, IEquatable<RGBColor>
    {
        #region Other

        /// <summary>
        /// sRGB color space.
        /// Used when working space is not specified explicitly.
        /// </summary>
        public static readonly IRGBWorkingSpace DefaultWorkingSpace = RGBWorkingSpaces.sRGB;

        #endregion

        #region Constructor

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public RGBColor(in double r, in double g, in double b)
            : this(in r, in g, in b, in DefaultWorkingSpace)
        {
        }

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public RGBColor(in double r, in double g, in double b, in IRGBWorkingSpace workingSpace)
        {
            R = r.CheckRange(0, 1);
            G = g.CheckRange(0, 1);
            B = b.CheckRange(0, 1);
            _workingSpace = workingSpace;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public RGBColor(in double[] vector)
            : this(vector, DefaultWorkingSpace)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public RGBColor(in double[] vector, in IRGBWorkingSpace workingSpace)
            : this(in vector[0], in vector[1], in vector[2], in workingSpace)
        {
        }

#if (DRAWING)
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public RGBColor(in Color color)
            : this(in color, in DefaultWorkingSpace)
        {
        }

        /// <param name="color"></param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public RGBColor(in Color color, in IRGBWorkingSpace workingSpace)
            : this(color.R / 255d, color.G / 255d, color.B / 255d, in workingSpace)
        {
        }

#endif

        #endregion

        #region Channels

        /// <summary>
        /// Red
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        public double R { get; }

        /// <summary>
        /// Green
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        public double G { get; }

        /// <summary>
        /// Blue
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        public double B { get; }

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public double[] Vector => new[] { R, G, B };

        #endregion

        #region Attributes

        /// <summary>
        /// RGB color space
        /// <seealso cref="RGBWorkingSpaces" />
        /// </summary>
        public IRGBWorkingSpace WorkingSpace => _workingSpace ?? DefaultWorkingSpace;

        private readonly IRGBWorkingSpace _workingSpace;

        #endregion

        #region Equality

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(RGBColor other) =>
            R == other.R &&
            G == other.G &&
            B == other.B &&
            WorkingSpace.Equals(other.WorkingSpace);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is RGBColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ WorkingSpace.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBColor left, RGBColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBColor left, RGBColor right) => !Equals(left, right);

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public static RGBColor FromGrey(in double value, in IRGBWorkingSpace workingSpace) => new RGBColor(in value, in value, in value, in workingSpace);

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public static RGBColor FromGrey(in double value) => FromGrey(in value, in DefaultWorkingSpace);

        /// <summary>
        /// Creates RGB color from 8-bit channels
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public static RGBColor FromRGB8bit(in byte red, in byte green, in byte blue, in IRGBWorkingSpace workingSpace) => new RGBColor(red / 255d, green / 255d, blue / 255d, in workingSpace);


        /// <summary>
        /// Creates RGB color from 8-bit channels
        /// </summary>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public static RGBColor FromRGB8bit(in byte red, in byte green, in byte blue) => FromRGB8bit(in red, in green, in blue, in DefaultWorkingSpace);

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
            var r = (byte)Math.Round(input.R * 255).CropRange(0, 255);
            var g = (byte)Math.Round(input.G * 255).CropRange(0, 255);
            var b = (byte)Math.Round(input.B * 255).CropRange(0, 255);
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