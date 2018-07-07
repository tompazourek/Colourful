namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="xyYColor" /> to <see cref="XYZColor" /> and back.
    /// </summary>
    public sealed class xyYAndXYZConverter : IColorConversion<XYZColor, xyYColor>, IColorConversion<xyYColor, XYZColor>
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
        public xyYColor Convert(in XYZColor input)
        {
            var x = input.X / (input.X + input.Y + input.Z);
            var y = input.Y / (input.X + input.Y + input.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(0, 0, input.Y);

            var Y = input.Y;
            return new xyYColor(x, y, Y);
        }
    }
}