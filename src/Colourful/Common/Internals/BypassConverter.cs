namespace Colourful.Internals
{
    public class BypassConverter<TColor> : IColorConverter<TColor, TColor>
        where TColor : IColorSpace
    {
        public TColor Convert(in TColor sourceColor) => sourceColor;
    }
}