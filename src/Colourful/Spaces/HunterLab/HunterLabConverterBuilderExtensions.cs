using Colourful.Internals;

namespace Colourful
{
    public static class HunterLabConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<HunterLabColor> FromHunterLab(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<HunterLabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<HunterLabColor> FromHunterLab(this ConverterBuilder builder)
            => builder.FromHunterLab(Illuminants.C);

        public static IFluentConverterBuilderFromTo<TSource, HunterLabColor> ToHunterLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<HunterLabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, HunterLabColor> ToHunterLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToHunterLab(Illuminants.C);
    }
}