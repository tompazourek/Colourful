using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Strategy.Rules
{
    public class Bypass_EqWhitePoint<TColor> : IConversionRule<TColor, TColor>
        where TColor : struct
    {
        public virtual int Priority => ConversionRulePriorities.Bypass;

        public virtual bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IColorConversion<TColor, TColor> conversion)
        {
            conversion = null;

            if (!EqualWhitePoints(in sourceNode, in targetNode))
                return false;

            conversion = new BypassConversion<TColor>();
            return true;
        }
    }
}