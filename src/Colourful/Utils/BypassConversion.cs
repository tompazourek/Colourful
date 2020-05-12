namespace Colourful.Strategy.Rules
{
    public class BypassConversion<TColor> : IColorConversion<TColor, TColor>
        where TColor : struct
    {
        public TColor Convert(in TColor sourceColor) => sourceColor;
    }
}