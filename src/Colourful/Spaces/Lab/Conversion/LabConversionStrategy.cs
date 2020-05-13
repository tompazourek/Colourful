using Colourful.Strategy;
using Colourful.Strategy.Rules;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class LabConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : struct
        {
            // only process Lab
            if (typeof(TColor) != typeof(LabColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<LabColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // Lab{WP1} -> XYZ{WP1}
            if (typeof(TSource) == typeof(LabColor) && typeof(TTarget) == typeof(XYZColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new LabToXYZConverter(sourceNode.GetWhitePointRequired()) as IColorConverter<TSource, TTarget>;
                }
            }
            // XYZ{WP1} -> Lab{WP1}
            else if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(LabColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new XYZToLabConverter(targetNode.GetWhitePointRequired()) as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // Lab{WP1} -> any = Lab{WP1} -> XYZ{WP1} -> any
            if (typeof(TSource) == typeof(LabColor))
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
            // any -> Lab{WP1} = any -> XYZ{WP1} -> Lab{WP1}
            if (typeof(TSource) == typeof(LabColor))
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