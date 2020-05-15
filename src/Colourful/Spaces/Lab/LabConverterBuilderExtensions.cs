using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class LabConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="LabColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Lab, the <see cref="Illuminants.D50" /> is usually used.</param>
        public static IFluentConverterBuilderFrom<LabColor> FromLab(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="LabColor" />.
        /// Assuming the usual <see cref="Illuminants.D50" /> white point for Lab.
        /// </summary>
        public static IFluentConverterBuilderFrom<LabColor> FromLab(this ConverterBuilder builder)
            => builder.FromLab(Illuminants.D50);

        /// <summary>
        /// Specifies that the target space is <see cref="LabColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Lab, the <see cref="Illuminants.D50" /> is usually used.</param>
        public static IFluentConverterBuilderFromTo<TSource, LabColor> ToLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="LabColor" />.
        /// Assuming the usual <see cref="Illuminants.D50" /> white point for Lab.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, LabColor> ToLab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLab(Illuminants.D50);
    }
}