using Colourful.Strategy;
using Colourful.Utils;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class LChabConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : struct
        {
            // only process LChab
            if (typeof(TColor) != typeof(LChabColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<LChabColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // LChab{WP1} -> Lab{WP1}
            if (typeof(TSource) == typeof(LChabColor) && typeof(TTarget) == typeof(LabColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new LChabToLabConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // Lab{WP1} -> LChab{WP1}
            else if (typeof(TSource) == typeof(LabColor) && typeof(TTarget) == typeof(LChabColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new LabToLChabConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // LChab{WP1} -> any = LChab{WP1} -> Lab{WP1} -> any
            if (typeof(TSource) == typeof(LChabColor))
            {
                var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LabColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LabColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LabColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct 
            where TTarget : struct
        {
            // any -> LChab{WP1} = any -> Lab{WP1} -> LChab{WP1}
            if (typeof(TTarget) == typeof(LChabColor))
            {
                var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LabColor>(in sourceMetadata, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LabColor, TTarget>(intermediateNode, in targetMetadata);
                return new CompositeConverter<TSource, LabColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}