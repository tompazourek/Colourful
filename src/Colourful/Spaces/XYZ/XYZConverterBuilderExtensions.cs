using Colourful.Internals;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful
{
    /// <summary>
    /// Extensions for the <see cref="ConverterBuilder" /> fluent interface.
    /// </summary>
    public static class XYZConverterBuilderExtensions
    {
        /// <summary>
        /// Specifies that the source space is <see cref="XYZColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For XYZ, there is no default.</param>
        public static IFluentConverterBuilderFrom<XYZColor> FromXYZ(this ConverterBuilder builder, in XYZColor? whitePoint)
            => builder.From<XYZColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the source space is <see cref="XYZColor" />.
        /// Note that there's no white points specified, but it might be needed.
        /// </summary>
        public static IFluentConverterBuilderFrom<XYZColor> FromXYZ(this ConverterBuilder builder)
            => builder.FromXYZ(whitePoint: null);

        /// <summary>
        /// Specifies that the target space is <see cref="XYZColor" />.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="whitePoint">Optionally, you can set a white point. For XYZ, there is no default.</param>
        public static IFluentConverterBuilderFromTo<TSource, XYZColor> ToXYZ<TSource>(this IFluentConverterBuilderFrom<TSource> builder, in XYZColor? whitePoint)
            where TSource : IColorSpace
            => builder.To<XYZColor>(new ConversionMetadata(CreateWhitePoint(in whitePoint)));

        /// <summary>
        /// Specifies that the target space is <see cref="XYZColor" />.
        /// Note that there's no white points specified, but it might be needed.
        /// </summary>
        public static IFluentConverterBuilderFromTo<TSource, XYZColor> ToXYZ<TSource>(this IFluentConverterBuilderFrom<TSource> builder)
            where TSource : IColorSpace
            => builder.ToXYZ(whitePoint: null);
    }
}