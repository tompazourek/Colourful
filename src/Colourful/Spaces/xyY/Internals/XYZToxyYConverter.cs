namespace Colourful.Internals
{
    /// <inheritdoc />
    public class XYZToxyYConverter : IColorConverter<XYZColor, xyYColor>
    {
        /// <inheritdoc />
        public xyYColor Convert(in XYZColor sourceColor)
        {
            var x = sourceColor.X / (sourceColor.X + sourceColor.Y + sourceColor.Z);
            var y = sourceColor.Y / (sourceColor.X + sourceColor.Y + sourceColor.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(x: 0, y: 0, sourceColor.Y);

            var Y = sourceColor.Y;
            var targetColor = new xyYColor(in x, in y, in Y);
            return targetColor;
        }
    }
}