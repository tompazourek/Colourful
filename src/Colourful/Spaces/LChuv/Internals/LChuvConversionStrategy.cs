using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals
{
    public class LChuvConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace
        {
            // only process LChuv
            if (typeof(TColor) != typeof(LChuvColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<LChuvColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // LChuv{WP1} -> Luv{WP1}
            if (typeof(TSource) == typeof(LChuvColor) && typeof(TTarget) == typeof(LuvColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new LChuvToLuvConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // Luv{WP1} -> LChuv{WP1}
            else if (typeof(TSource) == typeof(LuvColor) && typeof(TTarget) == typeof(LChuvColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new LuvToLChuvConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // LChuv{WP1} -> any = LChuv{WP1} -> Luv{WP1} -> any
            if (typeof(TSource) == typeof(LChuvColor))
            {
                var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, LuvColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<LuvColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LuvColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            // any -> LChuv{WP1} = any -> Luv{WP1} -> LChuv{WP1}
            if (typeof(TTarget) == typeof(LChuvColor))
            {
                var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                var firstConversion = converterAbstractFactory.CreateConverter<TSource, LuvColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterAbstractFactory.CreateConverter<LuvColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LuvColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}