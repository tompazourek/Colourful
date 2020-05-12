namespace Colourful.Strategy
{
    /// <summary>
    /// Rule for the conversion strategy builder.
    /// </summary>
    public interface IConversionRule<TSource, TTarget> 
        where TSource : struct 
        where TTarget : struct
    {
        int Priority { get; }

        /// <param name="sourceNode">Conversion from this</param>
        /// <param name="targetNode">Conversion to this</param>
        /// <param name="conversion">If rule applies, this will contain a conversion (<see cref="T:Colourful.IColorConversion`2" />).</param>
        /// <returns>True if the rule applies (resolved conversion), false if it doesn't (no conversion).</returns>
        bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IColorConversion<TSource, TTarget> conversion);
    }
}