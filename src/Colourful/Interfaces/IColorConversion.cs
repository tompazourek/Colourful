namespace Colourful
{
    /// <summary>
    /// Converts color between two color spaces.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IColorConversion<TInput, out TOutput>
        where TInput : struct
        where TOutput : struct
    {
        /// <summary>
        /// Converts from the input color space to the output color space.
        /// </summary>
        TOutput Convert(in TInput sourceColor);
    }
}