using Colourful.Internals;

namespace Colourful
{
    public static class LabConverterBuilderExtensions
    {
        public static IFluentConverterBuilderFrom<LabColor> FromLab(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFrom<LabColor> FromLab(this ConverterBuilder builder)
            => builder.FromLab(Illuminants.D50);

        public static IFluentConverterBuilderFromTo<TSource, LabColor> ToLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LabColor>(new ConversionMetadata(ConversionMetadataUtils.CreateWhitePoint(in whitePoint)));

        public static IFluentConverterBuilderFromTo<TSource, LabColor> ToLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLab(Illuminants.D50);
    }
}