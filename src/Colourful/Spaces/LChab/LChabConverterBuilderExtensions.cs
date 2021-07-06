using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class LChabConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="LChabColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For LChab, the <see cref="Illuminants.D50" /> is usually used.</param>
        public static IFluentConverterBuilderFrom<LChabColor> FromLChab(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LChabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="LChabColor" />.
        /// Assuming the usual <see cref="Illuminants.D50" /> white point for LChab.
        /// </summary>
        public static IFluentConverterBuilderFrom<LChabColor> FromLChab(this ConverterBuilder builder)
            => builder.FromLChab(Illuminants.D50);

        /// <summary>
        /// Specifies that the target space is <see cref="LChabColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For LChab, the <see cref="Illuminants.D50" /> is usually used.</param>
        public static IFluentConverterBuilderFromTo<TSource, LChabColor> ToLChab<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LChabColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="LChabColor" />.
        /// Assuming the usual <see cref="Illuminants.D50" /> white point for LChab.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, LChabColor> ToLChab<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLChab(Illuminants.D50);
    }
}
