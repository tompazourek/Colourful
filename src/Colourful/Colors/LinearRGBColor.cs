using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Colourful.Implementation;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>, which has linear channels (not companded)
    /// </summary>
    public readonly struct LinearRGBColor : IColorVector
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
        public LinearRGBColor(double r, double g, double b)
            : this(r, g, b, DefaultWorkingSpace)
        {
        }

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public LinearRGBColor(double r, double g, double b, IRGBWorkingSpace workingSpace)
        {
            R = r.CheckRange(0, 1);
            G = g.CheckRange(0, 1);
            B = b.CheckRange(0, 1);
            _workingSpace = workingSpace;
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public LinearRGBColor(Vector vector)
            : this(vector, DefaultWorkingSpace)
        {
        }

        /// <param name="vector"><see cref="Vector" />, expected 3 dimensions (range from 0 to 1)</param>
        /// <param name="workingSpace">
        /// <see cref="RGBWorkingSpaces" />
        /// </param>
        public LinearRGBColor(Vector vector, IRGBWorkingSpace workingSpace)
            : this(vector[0], vector[1], vector[2], workingSpace)
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
        public Vector Vector => new[] { R, G, B };

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

        /// <inheritdoc cref="object" />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(LinearRGBColor other) =>
            R == other.R &&
            G == other.G &&
            B == other.B &&
            WorkingSpace.Equals(other.WorkingSpace);

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LinearRGBColor other && Equals(other);

        /// <inheritdoc cref="object" />
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
        public static LinearRGBColor FromGrey(double value, IRGBWorkingSpace workingSpace) => new LinearRGBColor(value, value, value, workingSpace);

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace" /> as working space.</remarks>
        public static LinearRGBColor FromGrey(double value) => FromGrey(value, DefaultWorkingSpace);

        #endregion

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "LinearRGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);

        #endregion
    }
}