using Colourful.Strategy;
using Colourful.Strategy.Rules;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class xyYConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : struct
        {
            // only process xyY
            if (typeof(TColor) != typeof(xyYColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<xyYColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // xyY{WP1} -> XYZ{WP1}
            if (typeof(TSource) == typeof(xyYColor) && typeof(TTarget) == typeof(XYZColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new XyYToXYZConverter() as IColorConverter<TSource, TTarget>;
                }
            }
            // XYZ{WP1} -> xyY{WP1}
            else if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(xyYColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new XYZToxyYConverter() as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // xyY{WP1} -> any = xyY{WP1} -> XYZ{WP1} -> any
            if (typeof(TSource) == typeof(xyYColor))
            {
                var intermediateNode = new ConversionMetadata(sourceNode.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, XYZColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct 
            where TTarget : struct
        {
            // any -> xyY{WP1} = any -> XYZ{WP1} -> xyY{WP1}
            if (typeof(TSource) == typeof(xyYColor))
            {
                var intermediateNode = new ConversionMetadata(targetNode.GetWhitePointItem());
                var firstConversion = converterFactory.CreateConverter<TSource, XYZColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}