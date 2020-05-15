using Colourful.Internals;

namespace Colourful
{
    public static class LuvConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LuvColor> FromLuv(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LuvColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<LuvColor> FromLuv(this ConverterBuilder builder)
            => builder.FromLuv(Illuminants.D65);

        public static IFluentConverterBuilderFromTo<TSource, LuvColor> ToLuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LuvColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, LuvColor> ToLuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLuv(Illuminants.D65);
    }
}