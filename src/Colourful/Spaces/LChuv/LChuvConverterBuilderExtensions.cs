using Colourful.Internals;

namespace Colourful
{
    public static class LChuvConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LChuvColor> FromLChuv(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LChuvColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<LChuvColor> FromLChuv(this ConverterBuilder builder)
            => builder.FromLChuv(Illuminants.D65);

        public static IFluentConverterBuilderFromTo<TSource, LChuvColor> ToLChuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LChuvColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, LChuvColor> ToLChuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLChuv(Illuminants.D65);
    }
}