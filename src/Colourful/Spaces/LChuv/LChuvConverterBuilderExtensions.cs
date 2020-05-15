using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class LChuvConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="LChuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For LChuv, the <see cref="Illuminants.D65" /> is usually used.</param>
        public static IFluentConverterBuilderFrom<LChuvColor> FromLChuv(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LChuvColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="LChuvColor" />.
        /// Assuming the usual <see cref="Illuminants.D65" /> white point for LChuv.
        /// </summary>
        public static IFluentConverterBuilderFrom<LChuvColor> FromLChuv(this ConverterBuilder builder)
            => builder.FromLChuv(Illuminants.D65);

        /// <summary>
        /// Specifies that the target space is <see cref="LChuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For LChuv, the <see cref="Illuminants.D65" /> is usually used.</param>
        public static IFluentConverterBuilderFromTo<TSource, LChuvColor> ToLChuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LChuvColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="LChuvColor" />.
        /// Assuming the usual <see cref="Illuminants.D65" /> white point for LChuv.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, LChuvColor> ToLChuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLChuv(Illuminants.D65);
    }
}