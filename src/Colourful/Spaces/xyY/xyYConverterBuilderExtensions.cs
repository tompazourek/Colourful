using Colourful.Internals;

namespace Colourful
{
    public static class xyYConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<xyYColor> FromxyY(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<xyYColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<xyYColor> FromxyY(this ConverterBuilder builder)
            => builder.FromxyY(whitePoint: null);

        public static IFluentConverterBuilderFromTo<TSource, xyYColor> ToxyY<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<xyYColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, xyYColor> ToxyY<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToxyY(whitePoint: null);
    }
}