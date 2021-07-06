using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class JzCzhzConversionStrategy : IConversionStrategy
    {
        /// <inheritdoc />
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace
        {
            // only process JzCzhz
            if (typeof(TColor) != typeof(JzCzhzColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<JzCzhzColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // JzCzhz{WP1} -> Jzazbz{WP1}
            if (typeof(TSource) == typeof(JzCzhzColor) && typeof(TTarget) == typeof(JzazbzColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new JzCzhzToJzazbzConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // Jzazbz{WP1} -> JzCzhz{WP1}
            else if (typeof(TSource) == typeof(JzazbzColor) && typeof(TTarget) == typeof(JzCzhzColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new JzazbzToJzCzhzConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // JzCzhz{WP1} -> any = JzCzhz{WP1} -> Jzazbz{WP1} -> any
            if (typeof(TSource) == typeof(JzCzhzColor))
            {
                var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, JzazbzColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<JzazbzColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, JzazbzColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // any -> JzCzhz{WP1} = any -> Jzazbz{WP1} -> JzCzhz{WP1}
            if (typeof(TTarget) == typeof(JzCzhzColor))
            {
                var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, JzazbzColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<JzazbzColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, JzazbzColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}
