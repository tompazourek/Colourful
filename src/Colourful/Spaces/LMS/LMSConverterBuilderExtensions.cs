using Colourful.Internals;

namespace Colourful
{
    public static class LMSConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LMSColor> FromLMS(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LMSColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<LMSColor> FromLMS(this ConverterBuilder builder)
            => builder.FromLMS(whitePoint: null);

        public static IFluentConverterBuilderFromTo<TSource, LMSColor> ToLMS<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LMSColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, LMSColor> ToLMS<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLMS(whitePoint: null);
    }
}