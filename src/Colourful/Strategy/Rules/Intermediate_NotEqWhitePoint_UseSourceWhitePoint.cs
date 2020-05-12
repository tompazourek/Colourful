using static Colourful.Strategy.ConversionRulePriorities;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Strategy.Rules
{
    /// <summary>
    /// If the conversion is between the same type and the white points are not equal, adds an intermediate node.
    /// The intermediate node will use the source white point.
    /// </summary>
    /// <typeparam name="TColor">Source and target color type</typeparam>
    /// <typeparam name="TIntermediate">Intermediate color type</typeparam>
    public class Intermediate_NotEqWhitePoint_UseSourceWhitePoint<TColor, TIntermediate> : IConversionRule
        where TColor : struct
        where TIntermediate : struct
    {
        /// <inheritdoc />
        public int Priority => Convert;

        /// <inheritdoc />
        public bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion)
        {
            replacementNodes = new IConversionMetadata[] { };
            conversion = null;

            if (!HaveColorTypes<TColor, TColor>(in sourceNode, in targetNode))
                return false;

            if (EqualWhitePoints(in sourceNode, in targetNode))
                return false;

            var intermediateNode = new ConversionMetadata(CreateColorType<TIntermediate>(), CreateWhitePoint(sourceNode.GetWhitePoint()));
            replacementNodes = new[] { sourceNode, intermediateNode, targetNode };
            return true;
        }
    }
}