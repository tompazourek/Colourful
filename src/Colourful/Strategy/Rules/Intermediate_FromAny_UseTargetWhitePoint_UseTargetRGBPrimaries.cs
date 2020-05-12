using static Colourful.Strategy.ConversionRulePriorities;
using static Colourful.Strategy.Rules.ConversionRuleUtils;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// If the conversion is to arbitrary type, adds an intermediate node.
    /// The intermediate node will use the target white point and RGB primaries.
    /// </summary>
    /// <typeparam name="TTarget">Source color type</typeparam>
    /// <typeparam name="TIntermediate">Intermediate color type</typeparam>
    public class Intermediate_FromAny_UseTargetWhitePoint_UseTargetRGBPrimaries<TIntermediate, TTarget> : IConversionRule
        where TIntermediate : struct
        where TTarget : struct
    {
        /// <inheritdoc />
        public int Priority => IntermediateFromAny;

        /// <inheritdoc />
        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion)
        {
            replacementNodes = new IConversionMetadata[] { };
            conversion = null;

            if (!targetNode.HasColorType<TTarget>())
                return false;

            var intermediateNode = new ConversionMetadata(CreateColorType<TIntermediate>(), CreateWhitePoint(targetNode.GetWhitePoint()), CreateRGBPrimaries(targetNode.GetRGBPrimaries()));
            replacementNodes = new[] { sourceNode, intermediateNode, targetNode };
            return true;
        }
    }
}