namespace Colourful.Internals;

/// <inheritdoc />
public class xyYToxyConverter : IColorConverter<xyYColor, xyChromaticity>
{
    /// <inheritdoc />
    public xyChromaticity Convert(in xyYColor sourceColor) => sourceColor.Chromaticity;
}
