using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class RGBConversionStrategy : IConversionStrategy
    {
        /// <inheritdoc />
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace
        {
            // only process RGB
            if (typeof(TColor) != typeof(RGBColor))
                return null;

            // if same WP, primaries and companding, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata) && EqualRGBPrimaries(in sourceMetadata, in targetMetadata) && EqualCompanding(in sourceMetadata, in targetMetadata))
                return new BypassConverter<RGBColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // RGB{WP1, primaries1} -> LinearRGB{WP1, primaries1}
            if (typeof(TSource) == typeof(RGBColor) && typeof(TTarget) == typeof(LinearRGBColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata) && EqualRGBPrimaries(in sourceMetadata, in targetMetadata))
                {
                    return new RGBToLinearRGBConverter(sourceMetadata.GetCompandingRequired()) as IColorConverter<TSource, TTarget>;
                }
            }
            // LinearRGB{WP1, primaries1} -> RGB{WP1, primaries1}
            else if (typeof(TSource) == typeof(LinearRGBColor) && typeof(TTarget) == typeof(RGBColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata) && EqualRGBPrimaries(in sourceMetadata, in targetMetadata))
                {
                    return new LinearRGBToRGBConverter(targetMetadata.GetCompanding()) as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // RGB{WP1, primaries1} -> any = RGB{WP1, primaries1} -> LinearRGB{WP1, primaries1} -> any
            if (typeof(TSource) == typeof(RGBColor))
            {
                var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem(), sourceMetadata.GetRGBPrimariesItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, LinearRGBColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<LinearRGBColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LinearRGBColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // any -> RGB{WP1, primaries1} = any -> LinearRGB{WP1, primaries1} -> RGB{WP1, primaries1}
            if (typeof(TTarget) == typeof(RGBColor))
            {
                var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem(), targetMetadata.GetRGBPrimariesItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, LinearRGBColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<LinearRGBColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LinearRGBColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}
