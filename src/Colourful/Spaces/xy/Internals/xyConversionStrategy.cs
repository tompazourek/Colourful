using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class xyConversionStrategy : IConversionStrategy
    {
        /// <inheritdoc />
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace
        {
            // only process xy
            if (typeof(TColor) != typeof(xyChromaticity))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<xyChromaticity>() as IColorConverter<TColor, TColor>;

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // xy{WP1} -> xyY{WP1}
            if (typeof(TSource) == typeof(xyChromaticity) && typeof(TTarget) == typeof(xyYColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new xyToxyYConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // xyY{WP1} -> xy{WP1}
            else if (typeof(TSource) == typeof(xyYColor) && typeof(TTarget) == typeof(xyChromaticity))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new xyYToxyConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // xy{WP1} -> any = xy{WP1} -> xyY{WP1} -> any
            if (typeof(TSource) == typeof(xyChromaticity))
            {
                var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, xyYColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<xyYColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, xyYColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // any -> xy{WP1} = any -> xyY{WP1} -> xy{WP1}
            if (typeof(TTarget) == typeof(xyChromaticity))
            {
                var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, xyYColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<xyYColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, xyYColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}