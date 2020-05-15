using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class JzazbzConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="JzazbzColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Jzazbz, the <see cref="Illuminants.D65" /> is used.</param>
        public static IFluentConverterBuilderFrom<JzazbzColor> FromJzazbz(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<JzazbzColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="JzazbzColor" />.
        /// Assuming the <see cref="Illuminants.D65" /> white point for Jzazbz.
        /// </summary>
        public static IFluentConverterBuilderFrom<JzazbzColor> FromJzazbz(this ConverterBuilder builder)
            => builder.FromJzazbz(Illuminants.D65);

        /// <summary>
        /// Specifies that the target space is <see cref="JzazbzColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For Jzazbz, the <see cref="Illuminants.D65" /> is used.</param>
        public static IFluentConverterBuilderFromTo<TSource, JzazbzColor> ToJzazbz<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<JzazbzColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="JzazbzColor" />.
        /// Assuming the <see cref="Illuminants.D65" /> white point for Jzazbz.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, JzazbzColor> ToJzazbz<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToJzazbz(Illuminants.D65);
    }
}