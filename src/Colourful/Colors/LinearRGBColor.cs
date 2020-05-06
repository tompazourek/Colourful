using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Implementation;

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>, which has linear channels (not companded)
    /// </summary>
    public readonly struct LinearRGBColor : IColorVector, IEquatable<LinearRGBColor>
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
        public LinearRGBColor(in double r, in double g, in double b)
            : this(r, g, b, DefaultWorkingSpace)
        {
        }

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public LinearRGBColor(in double r, in double g, in double b, in IRGBWorkingSpace workingSpace)
        {
            R = r.CheckRange(0, 1);
            G = g.CheckRange(0, 1);
            B = b.CheckRange(0, 1);
            _workingSpace = workingSpace;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public LinearRGBColor(in double[] vector)
            : this(in vector, in DefaultWorkingSpace)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public LinearRGBColor(in double[] vector, in IRGBWorkingSpace workingSpace)
            : this(in vector[0], in vector[1], in vector[2], in workingSpace)
        {
        }

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
        public bool Equals(LinearRGBColor other) =>
            R == other.R &&
            G == other.G &&
            B == other.B &&
            WorkingSpace.Equals(other.WorkingSpace);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LinearRGBColor other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ WorkingSpace.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LinearRGBColor left, LinearRGBColor right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LinearRGBColor left, LinearRGBColor right) => !Equals(left, right);

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public static LinearRGBColor FromGrey(in double value, in IRGBWorkingSpace workingSpace) => new LinearRGBColor(in value, in value, in value, in workingSpace);

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public static LinearRGBColor FromGrey(in double value) => FromGrey(in value, in DefaultWorkingSpace);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LinearRGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);

        #endregion
    }
}