using static Colourful.Strategy.ConversionRulePriorities;
using static Colourful.Strategy.Rules.ConversionRuleUtils;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// If the color type, white point, RGB primaries, and companding are equal, replaces the two nodes with the source node.
    /// </summary>
    public class Return_EqSpace_EqWhitePoint_EqRGBPrimaries_EqCompanding<TColor> : IConversionRule
        where TColor : struct
    {
        /// <inheritdoc />
        public int Priority => Return;

        /// <inheritdoc />
        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion)
        {
            replacementNodes = new IConversionMetadata[] { };
            conversion = null;

            if (!HaveColorTypes<TColor, TColor>(in sourceNode, in targetNode))
                return false;

            if (!EqualWhitePoints(in sourceNode, in targetNode))
                return false;
            
            if (!EqualRGBPrimaries(in sourceNode, in targetNode))
                return false;
            
            if (!EqualCompanding(in sourceNode, in targetNode))
                return false;

            replacementNodes = new[] { sourceNode };
            return true;
        }
    }
}