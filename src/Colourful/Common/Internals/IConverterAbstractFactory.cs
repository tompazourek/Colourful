namespace Colourful.Internals
{
    /// <summary>
    /// Processes conversion strategies and creates the converters.
    /// It is an implementation hidden behind the more friendly <see cref="ConverterBuilder" /> API.
    /// </summary>
    public interface IConverterAbstractFactory
    {
        /// <summary>
        /// Creates a converter between the source and target color spaces.
        /// If the conversion cannot be done, it crashes.
        /// </summary>
        /// <param name="sourceMetadata">Metadata about the source space.</param>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <typeparam name="TSource">Source space.</typeparam>
        /// <typeparam name="TTarget">Target space.</typeparam>
        /// <returns>Converter that can be repeatedly used to convert colors.</returns>
        IColorConverter<TSource, TTarget> CreateConverter<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata)
            where TSource : IColorSpace
            where TTarget : IColorSpace;
    }
}