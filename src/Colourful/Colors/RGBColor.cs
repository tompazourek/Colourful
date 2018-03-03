using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;
#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif
#if (DRAWING)
using System.Drawing;
using Colourful.Implementation;

#endif

namespace Colourful
{
    /// <summary>
    /// RGB color with specified <see cref="IRGBWorkingSpace">working space</see>
    /// </summary>
    public class RGBColor : RGBColorBase
    {
        #region Other

        /// <summary>
        /// sRGB color space.
        /// Used when working space is not specified explicitly.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IRGBWorkingSpace DefaultWorkingSpace = RGBWorkingSpaces.sRGB;

        #endregion

        #region Constructor

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace"/> as working space.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        public RGBColor(double r, double g, double b)
            : this(r, g, b, DefaultWorkingSpace)
        {
        }

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        /// <param name="workingSpace"><see cref="RGBWorkingSpaces"/></param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        public RGBColor(double r, double g, double b, IRGBWorkingSpace workingSpace)
            : base(r, g, b)
        {
            WorkingSpace = workingSpace;
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions (range from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace"/> as working space.</remarks>
        public RGBColor(Vector vector)
            : this(vector, DefaultWorkingSpace)
        {
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions (range from 0 to 1)</param>
        /// <param name="workingSpace"><see cref="RGBWorkingSpaces"/></param>
        public RGBColor(Vector vector, IRGBWorkingSpace workingSpace)
            : base(vector)
        {
            WorkingSpace = workingSpace;
        }

#if (DRAWING)

        /// <remarks>Uses <see cref="DefaultWorkingSpace"/> as working space.</remarks>
        public RGBColor(Color color)
            : this(color, DefaultWorkingSpace)
        {
        }

        /// <param name="color"></param>
        /// <param name="workingSpace"><see cref="RGBWorkingSpaces"/></param>
        public RGBColor(Color color, IRGBWorkingSpace workingSpace)
            : base(((double)color.R)/255, ((double)color.G)/255, ((double)color.B)/255)
        {
            WorkingSpace = workingSpace;
        }

#endif

        #endregion

        #region Attributes

        /// <summary>
        /// RGB color space
        /// <seealso cref="RGBWorkingSpaces"/>
        /// </summary>
        public IRGBWorkingSpace WorkingSpace { get; }

        #endregion

        #region Equality

        /// <inheritdoc cref="object" />
        public bool Equals(RGBColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return base.Equals(other) && WorkingSpace.Equals(other.WorkingSpace);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBColor)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ WorkingSpace.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(RGBColor left, RGBColor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(RGBColor left, RGBColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Factory methods

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <param name="workingSpace"><see cref="RGBWorkingSpaces"/></param>
        public static RGBColor FromGrey(double value, IRGBWorkingSpace workingSpace)
        {
            return new RGBColor(value, value, value, workingSpace);
        }

        /// <summary>
        /// Creates RGB color with all channels equal
        /// </summary>
        /// <param name="value">Grey value (from 0 to 1)</param>
        /// <remarks>Uses <see cref="DefaultWorkingSpace"/> as working space.</remarks>
        public static RGBColor FromGrey(double value)
        {
            return FromGrey(value, DefaultWorkingSpace);
        }

        /// <summary>
        /// Creates RGB color from 8-bit channels
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="workingSpace"><see cref="RGBWorkingSpaces"/></param>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "bit")]
        public static RGBColor FromRGB8bit(byte red, byte green, byte blue, IRGBWorkingSpace workingSpace)
        {
            return new RGBColor(red/255d, green/255d, blue/255d, workingSpace);
        }


        /// <summary>
        /// Creates RGB color from 8-bit channels
        /// </summary>
        /// <remarks>Uses <see cref="DefaultWorkingSpace"/> as working space.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "bit")]
        public static RGBColor FromRGB8bit(byte red, byte green, byte blue)
        {
            return FromRGB8bit(red, green, blue, DefaultWorkingSpace);
        }

        #endregion

#if (DRAWING)

        #region Color conversions

        /// <summary>
        /// Convert to <see cref="System.Drawing.Color"/>.
        /// </summary>
        public Color ToColor()
        {
            return this;
        }

        /// <summary>
        /// Convert to <see cref="System.Drawing.Color"/>.
        /// </summary>
        public static implicit operator Color(RGBColor input)
        {
            if (input == null)
                return new Color();

            var r = (byte)Math.Round(input.R*255).CropRange(0, 255);
            var g = (byte)Math.Round(input.G*255).CropRange(0, 255);
            var b = (byte)Math.Round(input.B*255).CropRange(0, 255);
            var output = Color.FromArgb(r, g, b);
            return output;
        }

        /// <summary>
        /// Convert from <see cref="System.Drawing.Color"/>.
        /// </summary>
        public static explicit operator RGBColor(Color color)
        {
            var output = new RGBColor(color);
            return output;
        }

        #endregion

#endif

        #region Overrides

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "RGB [R={0:0.##}, G={1:0.##}, B={2:0.##}]", R, G, B);
        }

        #endregion
    }
}