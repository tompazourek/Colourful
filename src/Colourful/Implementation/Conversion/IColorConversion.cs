namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts color between two color spaces.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IColorConversion<in TInput, out TOutput>
    {
        /// <summary>
        /// Converts from the input color space to the output color space.
        /// </summary>
        TOutput Convert(TInput input);
    }
}