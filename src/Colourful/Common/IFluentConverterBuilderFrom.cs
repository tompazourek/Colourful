using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// Fluent interface for <see cref="ConverterBuilder" />.
    /// </summary>
    /// <typeparam name="TSource">Source space.</typeparam>
    public interface IFluentConverterBuilderFrom<TSource>
        where TSource : IColorSpace
    {
        /// <summary>
        /// Saves the metadata about the target color space, and proceeds.
        /// Intended only for internal implementation.
        /// Use the ToXXX extension methods.
        /// </summary>
        /// <typeparam name="TTarget">Target space.</typeparam>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <returns>Fluent interface.</returns>
        IFluentConverterBuilderFromTo<TSource, TTarget> To<TTarget>(IConversionMetadata targetMetadata)
            where TTarget : IColorSpace;
    }
}