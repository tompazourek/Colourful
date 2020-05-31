namespace Colourful.Internals
{
    /// <inheritdoc />
    public class xyToxyYConverter : IColorConverter<xyChromaticity, xyYColor>
    {
        /// <inheritdoc />
        public xyYColor Convert(in xyChromaticity sourceChromaticity) => new xyYColor(sourceChromaticity, Y: 1);
    }
}