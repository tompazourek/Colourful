using Colourful.Internals;

namespace Colourful
{
    public static class RGBConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<RGBColor> FromRGB(this ConverterBuilder builder, in IRGBWorkingSpace rgbWorkingSpace)
            => builder.From<RGBColor>(new ConversionMetadata
            (
                ConversionMetadataUtils.CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                ConversionMetadataUtils.CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                ConversionMetadataUtils.CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        public static IFluentConverterBuilderFrom<RGBColor> FromRGB(this ConverterBuilder builder)
            => builder.FromRGB(RGBWorkingSpaces.sRGB);

        public static IFluentConverterBuilderFromTo<TSource, RGBColor> ToRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in IRGBWorkingSpace rgbWorkingSpace)
            where TSource : IColorSpace
            => builder.To<RGBColor>(new ConversionMetadata
            (
                ConversionMetadataUtils.CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                ConversionMetadataUtils.CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                ConversionMetadataUtils.CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        public static IFluentConverterBuilderFromTo<TSource, RGBColor> ToRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToRGB(RGBWorkingSpaces.sRGB);
    }
}