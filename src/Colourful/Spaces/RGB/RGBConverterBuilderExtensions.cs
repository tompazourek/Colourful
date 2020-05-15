using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class RGBConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="LChuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="rgbWorkingSpace">Optionally, you can set an RGB working space. The <see cref="RGBWorkingSpaces.sRGB" /> is the usual default.</param>
        public static IFluentConverterBuilderFrom<RGBColor> FromRGB(this ConverterBuilder builder, in IRGBWorkingSpace rgbWorkingSpace)
            => builder.From<RGBColor>(new ConversionMetadata
            (
                CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        /// <summary>
        /// Specifies that the source space is <see cref="LChuvColor" />.
        /// Assuming the usual <see cref="RGBWorkingSpaces.sRGB" /> RGB working space.
        /// </summary>
        public static IFluentConverterBuilderFrom<RGBColor> FromRGB(this ConverterBuilder builder)
            => builder.FromRGB(RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Specifies that the target space is <see cref="LChuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="rgbWorkingSpace">Optionally, you can set an RGB working space. The <see cref="RGBWorkingSpaces.sRGB" /> is the usual default.</param>
        public static IFluentConverterBuilderFromTo<TSource, RGBColor> ToRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in IRGBWorkingSpace rgbWorkingSpace)
            where TSource : IColorSpace
            => builder.To<RGBColor>(new ConversionMetadata
            (
                CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        /// <summary>
        /// Specifies that the target space is <see cref="LChuvColor" />.
        /// Assuming the usual <see cref="RGBWorkingSpaces.sRGB" /> RGB working space.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, RGBColor> ToRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToRGB(RGBWorkingSpaces.sRGB);
    }
}