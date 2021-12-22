using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful;

/// <summary>
/// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
/// </summary>
public static class xyConverterBuilderExtensions
{
    /// <summary>
    /// Specifies that the source space is <see cref="xyChromaticity" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For xy, there is no default.</param>
    public static IFluentConverterBuilderFrom<xyChromaticity> Fromxy(this ConverterBuilder builder, in XYZColor? whitePoint)
        => builder.From<xyChromaticity>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the source space is <see cref="xyChromaticity" />.
    /// Note that there's no white points specified, but it might be needed.
    /// </summary>
    public static IFluentConverterBuilderFrom<xyChromaticity> Fromxy(this ConverterBuilder builder)
        => builder.Fromxy(whitePoint: null);

    /// <summary>
    /// Specifies that the target space is <see cref="xyChromaticity" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For xy, there is no default.</param>
    public static IFluentConverterBuilderFromTo<TSource, xyChromaticity> Toxy<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
        where TSource : IColorSpace
        => builder.To<xyChromaticity>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the target space is <see cref="xyChromaticity" />.
    /// Note that there's no white points specified, but it might be needed.
    /// </summary>
    public static IFluentConverterBuilderFromTo<TSource, xyChromaticity> Toxy<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
        where TSource : IColorSpace
        => builder.Toxy(whitePoint: null);
}
