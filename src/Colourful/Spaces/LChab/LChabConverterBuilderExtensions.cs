using Colourful.Internals;

namespace Colourful
{
    public static class LChabConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LChabColor> FromLChab(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LChabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<LChabColor> FromLChab(this ConverterBuilder builder)
            => builder.FromLChab(Illuminants.D50);

        public static IFluentConverterBuilderFromTo<TSource, LChabColor> ToLChab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LChabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, LChabColor> ToLChab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLChab(Illuminants.D50);
    }
}