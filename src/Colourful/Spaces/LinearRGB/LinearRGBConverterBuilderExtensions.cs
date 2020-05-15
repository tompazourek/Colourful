using Colourful.Internals;

namespace Colourful
{
    public static class LinearRGBConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LinearRGBColor> FromLinearRGB(this ConverterBuilder builder, in IRGBWorkingSpace rgbWorkingSpace)
            => builder.From<LinearRGBColor>(new ConversionMetadata
            (
                ConversionMetadataUtils.CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                ConversionMetadataUtils.CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                ConversionMetadataUtils.CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        public static IFluentConverterBuilderFrom<LinearRGBColor> FromLinearRGB(this ConverterBuilder builder)
            => builder.FromLinearRGB(RGBWorkingSpaces.sRGB);

        public static IFluentConverterBuilderFromTo<TSource, LinearRGBColor> ToLinearRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in IRGBWorkingSpace rgbWorkingSpace)
            where TSource : IColorSpace
            => builder.To<LinearRGBColor>(new ConversionMetadata
            (
                ConversionMetadataUtils.CreateWhitePoint(rgbWorkingSpace?.WhitePoint),
                ConversionMetadataUtils.CreateRGBPrimaries(rgbWorkingSpace?.Primaries),
                ConversionMetadataUtils.CreateCompanding(rgbWorkingSpace?.Companding)
            ));

        public static IFluentConverterBuilderFromTo<TSource, LinearRGBColor> ToLinearRGB<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLinearRGB(RGBWorkingSpaces.sRGB);
    }
}