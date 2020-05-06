using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="xyYColor" /> to <see cref="XYZColor" /> and back.
    /// </summary>
    public sealed class xyYAndXYZConverter : IColorConversion<XYZColor, xyYColor>, IColorConversion<xyYColor, XYZColor>, IEquatable<xyYAndXYZConverter>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly xyYAndXYZConverter Default = new xyYAndXYZConverter();

        /// <summary>
        /// Converts from <see cref="xyYColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in xyYColor input)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (input.y == 0)
                return new XYZColor(0, 0, input.Luminance);

            var X = input.x * input.Luminance / input.y;
            var Y = input.Luminance;
            var Z = (1 - input.x - input.y) * Y / input.y;

            return new XYZColor(in X, in Y, in Z);
        }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="xyYColor" />.
        /// </summary>
        public xyYColor Convert(in XYZColor input)
        {
            var x = input.X / (input.X + input.Y + input.Z);
            var y = input.Y / (input.X + input.Y + input.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(0, 0, input.Y);

            var Y = input.Y;
            return new xyYColor(in x, in y, in Y);
        }
        
        #region Equality
        
        /// <inheritdoc />
        public bool Equals(xyYAndXYZConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is xyYAndXYZConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(xyYAndXYZConverter left, xyYAndXYZConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(xyYAndXYZConverter left, xyYAndXYZConverter right) => !Equals(left, right);

        #endregion
    }
}