using Colourful.Internals;

namespace Colourful;

/// <summary>
/// Fluent interface for <see cref="ConverterBuilder" />.
/// </summary>
public interface IFluentConverterBuilder
{
    /// <summary>
    /// Saves the metadata about the source color space, and proceeds.
    /// Intended only for internal implementation.
    /// Use the FromXXX extension methods.
    /// </summary>
    /// <typeparam name="TSource">Source space.</typeparam>
    /// <param name="sourceMetadata">Metadata about the source space.</param>
    /// <returns>Fluent interface.</returns>
    IFluentConverterBuilderFrom<TSource> From<TSource>(IConversionMetadata sourceMetadata)
        where TSource : IColorSpace;
}
