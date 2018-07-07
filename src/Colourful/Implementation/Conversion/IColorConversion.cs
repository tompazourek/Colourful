using System.Diagnostics.CodeAnalysis;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts color between two color spaces.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public interface IColorConversion<TInput, TOutput>
        where TInput : struct
        where TOutput : struct
    {
        /// <summary>
        /// Converts from the input color space to the output color space.
        /// </summary>
        TOutput Convert(in TInput input);
    }
}