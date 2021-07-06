using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class JzCzhzConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="JzCzhzColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For JzCzhz, the <see cref="Illuminants.D65" /> is used.</param>
        public static IFluentConverterBuilderFrom<JzCzhzColor> FromJzCzhz(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<JzCzhzColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="JzCzhzColor" />.
        /// Assuming the <see cref="Illuminants.D65" /> white point for JzCzhz.
        /// </summary>
        public static IFluentConverterBuilderFrom<JzCzhzColor> FromJzCzhz(this ConverterBuilder builder)
            => builder.FromJzCzhz(Illuminants.D65);

        /// <summary>
        /// Specifies that the target space is <see cref="JzCzhzColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For JzCzhz, the <see cref="Illuminants.D65" /> is used.</param>
        public static IFluentConverterBuilderFromTo<TSource, JzCzhzColor> ToJzCzhz<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<JzCzhzColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="JzCzhzColor" />.
        /// Assuming the <see cref="Illuminants.D65" /> white point for JzCzhz.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, JzCzhzColor> ToJzCzhz<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToJzCzhz(Illuminants.D65);
    }
}
