using static Colourful.Strategy.ConversionRulePriorities;
using static Colourful.Strategy.Rules.ConversionRuleUtils;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// If the conversion is to arbitrary type and the white points are not equal, adds an intermediate node.
    /// The intermediate node will use the target white point.
    /// </summary>
    /// <typeparam name="TSource">Source color type</typeparam>
    /// <typeparam name="TIntermediate">Intermediate color type</typeparam>
    public class Intermediate_ToAny_NotEqWhitePoint_UseTargetWhitePoint<TSource, TIntermediate> : IConversionRule
        where TSource : struct
        where TIntermediate : struct
    {
        /// <inheritdoc />
        public int Priority => IntermediateToAny;

        /// <inheritdoc />
        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion)
        {
            replacementNodes = new IConversionMetadata[] { };
            conversion = null;

            if (!sourceNode.HasColorType<TSource>())
                return false;

            if (EqualWhitePoints(in sourceNode, in targetNode))
                return false;

            var intermediateNode = new ConversionMetadata(CreateColorType<TIntermediate>(), CreateWhitePoint(targetNode.GetWhitePoint()));
            replacementNodes = new[] { sourceNode, intermediateNode, targetNode };
            return true;
        }
    }
}