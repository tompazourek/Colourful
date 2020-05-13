using Colourful.Strategy;
using Colourful.Strategy.Rules;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class LChuvConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : struct
        {
            // only process LChuv
            if (typeof(TColor) != typeof(LChuvColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<LChuvColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // LChuv{WP1} -> Luv{WP1}
            if (typeof(TSource) == typeof(LChuvColor) && typeof(TTarget) == typeof(LuvColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new LChuvToLuvConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // Luv{WP1} -> LChuv{WP1}
            else if (typeof(TSource) == typeof(LuvColor) && typeof(TTarget) == typeof(LChuvColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new LuvToLChuvConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // LChuv{WP1} -> any = LChuv{WP1} -> Luv{WP1} -> any
            if (typeof(TSource) == typeof(LChuvColor))
            {
                var intermediateNode = new ConversionMetadata(sourceNode.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LuvColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LuvColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, LuvColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct 
            where TTarget : struct
        {
            // any -> LChuv{WP1} = any -> Luv{WP1} -> LChuv{WP1}
            if (typeof(TSource) == typeof(LChuvColor))
            {
                var intermediateNode = new ConversionMetadata(targetNode.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LuvColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LuvColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, LuvColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}