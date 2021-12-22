using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful;

/// <summary>
/// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
/// </summary>
public static class HunterLabConverterBuilderExtensions
{
    /// <summary>
    /// Specifies that the source space is <see cref="HunterLabColor" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For HunterLab, the <see cref="Illuminants.C" /> is usually used.</param>
    public static IFluentConverterBuilderFrom<HunterLabColor> FromHunterLab(this ConverterBuilder builder, in XYZColor? whitePoint)
        => builder.From<HunterLabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the source space is <see cref="HunterLabColor" />.
    /// Assuming the usual <see cref="Illuminants.C" /> white point for HunterLab.
    /// </summary>
    public static IFluentConverterBuilderFrom<HunterLabColor> FromHunterLab(this ConverterBuilder builder)
        => builder.FromHunterLab(Illuminants.C);

    /// <summary>
    /// Specifies that the target space is <see cref="HunterLabColor" />.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="whitePoint">Optionally, you can set a white point. For HunterLab, the <see cref="Illuminants.C" /> is usually used.</param>
    public static IFluentConverterBuilderFromTo<TSource, HunterLabColor> ToHunterLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
        where TSource : IColorSpace
        => builder.To<HunterLabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

    /// <summary>
    /// Specifies that the target space is <see cref="HunterLabColor" />.
    /// Assuming the usual <see cref="Illuminants.C" /> white point for HunterLab.
    /// </summary>
    public static IFluentConverterBuilderFromTo<TSource, HunterLabColor> ToHunterLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
        where TSource : IColorSpace
        => builder.ToHunterLab(Illuminants.C);
}
