namespace Colourful.Internals
{
    /// <summary>
    /// Composes first converter (from source to intermediate space) with second converter (from intermediate to target space)
    /// to create the final source to target converter.
    /// </summary>
    public class CompositeConverter<TSource, TIntermediate, TTarget> : IColorConverter<TSource, TTarget>
        where TSource : IColorSpace
        where TIntermediate : IColorSpace
        where TTarget : IColorSpace
    {
        /// <summary>
        /// First converter (from source to intermediate space).
        /// </summary>
        public IColorConverter<TSource, TIntermediate> FirstConverter { get; }

        /// <summary>
        /// Second converter (from intermediate to target space).
        /// </summary>
        public IColorConverter<TIntermediate, TTarget> SecondConverter { get; }

        /// <summary>
        /// Creates a composite converter from two converters.
        /// </summary>
        /// <param name="firstConverter">First converter (from source to intermediate space).</param>
        /// <param name="secondConverter">Second converter (from intermediate to target space).</param>
        public CompositeConverter(IColorConverter<TSource, TIntermediate> firstConverter, IColorConverter<TIntermediate, TTarget> secondConverter)
        {
            FirstConverter = firstConverter;
            SecondConverter = secondConverter;
        }

        /// <inheritdoc />
        public TTarget Convert(in TSource sourceColor) => SecondConverter.Convert(FirstConverter.Convert(sourceColor));
    }
}