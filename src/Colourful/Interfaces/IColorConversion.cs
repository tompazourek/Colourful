namespace Colourful
{
    /// <summary>
    /// Converts color between two color spaces.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IColorConversion<TSource, out TTarget>
        where TSource : struct
        where TTarget : struct
    {
        /// <summary>
        /// Converts from the input color space to the output color space.
        /// </summary>
        TTarget Convert(in TSource sourceColor);
    }
}