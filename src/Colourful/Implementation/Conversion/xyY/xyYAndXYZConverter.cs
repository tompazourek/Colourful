using System;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="xyYColor" /> to <see cref="XYZColor" /> and back.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "xy")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xy")]
    public class xyYAndXYZConverter : IColorConversion<XYZColor, xyYColor>, IColorConversion<xyYColor, XYZColor>
    {
        /// <summary>
        /// Converts from <see cref="xyYColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(xyYColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (input.y == 0)
                return new XYZColor(0, 0, input.Luminance);
            // ReSharper restore CompareOfFloatsByEqualityOperator

            var X = input.x * input.Luminance / input.y;
            var Y = input.Luminance;
            var Z = (1 - input.x - input.y) * Y / input.y;

            return new XYZColor(X, Y, Z);
        }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="xyYColor" />.
        /// </summary>
        public xyYColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var x = input.X / (input.X + input.Y + input.Z);
            var y = input.Y / (input.X + input.Y + input.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(0, 0, input.Y);

            var Y = input.Y;
            return new xyYColor(x, y, Y);
        }
    }
}