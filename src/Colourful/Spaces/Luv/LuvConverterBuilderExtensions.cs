using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class LuvConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="LuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Luv, the <see cref="Illuminants.D65" /> is usually used.</param>
        public static IFluentConverterBuilderFrom<LuvColor> FromLuv(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<LuvColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="LuvColor" />.
        /// Assuming the usual <see cref="Illuminants.D65" /> white point for Luv.
        /// </summary>
        public static IFluentConverterBuilderFrom<LuvColor> FromLuv(this ConverterBuilder builder)
            => builder.FromLuv(Illuminants.D65);

        /// <summary>
        /// Specifies that the target space is <see cref="LuvColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Luv, the <see cref="Illuminants.D65" /> is usually used.</param>
        public static IFluentConverterBuilderFromTo<TSource, LuvColor> ToLuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<LuvColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="LuvColor" />.
        /// Assuming the usual <see cref="Illuminants.D65" /> white point for Luv.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, LuvColor> ToLuv<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToLuv(Illuminants.D65);
    }
}
