namespace Colourful
{
    /// <summary>
    /// Fluent interface for <see cref="ConverterBuilder" />.
    /// </summary>
    /// <typeparam name="TSource">Source space.</typeparam>
    /// <typeparam name="TTarget">Target space.</typeparam>
    public interface IFluentConverterBuilderFromTo<TSource, out TTarget>
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        /// <summary>
        /// Builds the converter.
        /// </summary>
        /// <returns>Converter instance.</returns>
        IColorConverter<TSource, TTarget> Build();
    }
}
