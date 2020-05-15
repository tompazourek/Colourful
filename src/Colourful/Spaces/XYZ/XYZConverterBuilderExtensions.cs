using Colourful.Internals;

namespace Colourful
{
    public static class XYZConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<XYZColor> FromXYZ(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<XYZColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<XYZColor> FromXYZ(this ConverterBuilder builder)
            => builder.FromXYZ(whitePoint: null);

        public static IFluentConverterBuilderFromTo<TSource, XYZColor> ToXYZ<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<XYZColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, XYZColor> ToXYZ<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToXYZ(whitePoint: null);
    }
}