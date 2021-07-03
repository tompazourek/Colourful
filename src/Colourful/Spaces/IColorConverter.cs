namespace Colourful
{
    /// <summary>
    /// Converts color between two color spaces.
    /// </summary>
    /// <typeparam name="TSource">Source space.</typeparam>
    /// <typeparam name="TTarget">Target space.</typeparam>
    public interface IColorConverter<TSource, out TTarget>
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        /// <summary>
        /// Converts the color from the source space to the target space.
        /// </summary>
        TTarget Convert(in TSource sourceColor);
    }
}
