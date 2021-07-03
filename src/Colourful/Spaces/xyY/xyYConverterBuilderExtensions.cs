using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class xyYConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="xyYColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For xyY, there is no default.</param>
        public static IFluentConverterBuilderFrom<xyYColor> FromxyY(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<xyYColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="xyYColor" />.
        /// Note that there's no white points specified, but it might be needed.
        /// </summary>
        public static IFluentConverterBuilderFrom<xyYColor> FromxyY(this ConverterBuilder builder)
            => builder.FromxyY(whitePoint: null);

        /// <summary>
        /// Specifies that the target space is <see cref="xyYColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For xyY, there is no default.</param>
        public static IFluentConverterBuilderFromTo<TSource, xyYColor> ToxyY<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<xyYColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="xyYColor" />.
        /// Note that there's no white points specified, but it might be needed.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, xyYColor> ToxyY<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToxyY(whitePoint: null);
    }
}
