namespace Colourful.Strategy
{
    /// <summary>
    /// Rule for the conversion strategy builder.
    /// </summary>
    public interface IConversionRule
    {
        /// <summary>
        /// Low priority rules get processed first.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Returns true if the rule is applicable.
        /// </summary>
        /// <param name="sourceNode">Conversion from this</param>
        /// <param name="targetNode">Conversion to this</param>
        /// <param name="replacementNodes">If rule applies, contains the nodes that should replace the given two</param>
        /// <param name="conversion">If rule applies, this can optionally contain a conversion operation for the strategy (<see cref="IColorConversion{TInput,TOutput}" />).</param>
        /// <returns></returns>
        bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IConversionMetadata[] replacementNodes, out object conversion);
    }
}