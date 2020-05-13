namespace Colourful.Strategy
{
    public class BypassConverter<TColor> : IColorConverter<TColor, TColor>
        where TColor : struct
    {
        public TColor Convert(in TColor sourceColor) => sourceColor;
    }
}