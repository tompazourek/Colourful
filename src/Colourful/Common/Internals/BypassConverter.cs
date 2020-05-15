namespace Colourful.Internals
{
    /// <summary>
    /// Bypasses the conversions, returns the same input as output.
    /// </summary>
    public class BypassConverter<TColor> : IColorConverter<TColor, TColor>
        where TColor : IColorSpace
    {
        /// <inheritdoc />
        public TColor Convert(in TColor sourceColor) => sourceColor;
    }
}