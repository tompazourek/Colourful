using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful;

/// <summary>
/// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
/// </summary>
public static class LMSConverterBuilderExtensions
{
    /// <summary>
    /// Specifies that the source space is <see cref="LMSColor" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For LMS, there is no default.</param>
    public static IFluentConverterBuilderFrom<LMSColor> FromLMS(this ConverterBuilder builder, in XYZColor? whitePoint)
        => builder.From<LMSColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the source space is <see cref="LMSColor" />.
    /// Note that there's no white points specified, but it might be needed.
    /// </summary>
    public static IFluentConverterBuilderFrom<LMSColor> FromLMS(this ConverterBuilder builder)
        => builder.FromLMS(whitePoint: null);

    /// <summary>
    /// Specifies that the target space is <see cref="LMSColor" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For LMS, there is no default.</param>
    public static IFluentConverterBuilderFromTo<TSource, LMSColor> ToLMS<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
        where TSource : IColorSpace
        => builder.To<LMSColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the target space is <see cref="LMSColor" />.
    /// Note that there's no white points specified, but it might be needed.
    /// </summary>
    public static IFluentConverterBuilderFromTo<TSource, LMSColor> ToLMS<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
        where TSource : IColorSpace
        => builder.ToLMS(whitePoint: null);
}
