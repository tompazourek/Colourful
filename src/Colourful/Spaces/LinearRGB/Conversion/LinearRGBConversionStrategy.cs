using Colourful.Strategy.Rules;
using Colourful.Strategy;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class LinearRGBConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory) 
            where TColor : struct
        {
            // only process LinearRGB
            if (typeof(TColor) != typeof(LinearRGBColor)) 
                return null;

            // if same WP and primaries, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata) && EqualRGBPrimaries(in sourceMetadata, in targetMetadata))
            {
                return new BypassConverter<LinearRGBColor>() as IColorConverter<TColor, TColor>;
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory) 
            where TSource : struct
            where TTarget : struct
        {
            // LinearRGB{WP1} -> XYZ{WP1}
            if (typeof(TSource) == typeof(LinearRGBColor) && typeof(TTarget) == typeof(XYZColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new LinearRGBToXYZConverter(sourceNode.GetWhitePointRequired(), sourceNode.GetRGBPrimariesRequired()) as IColorConverter<TSource, TTarget>;
                }
            }
            // XYZ{WP1} -> LinearRGB{WP1}
            else if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(LinearRGBColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode))
                {
                    return new XYZToLinearRGBConverter(targetNode.GetWhitePointRequired(), targetNode.GetRGBPrimariesRequired()) as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory) 
            where TSource : struct
            where TTarget : struct
        {
            // LinearRGB{WP1} -> any = LinearRGB{WP1} -> XYZ{WP1} -> any
            if (typeof(TSource) == typeof(LinearRGBColor))
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
            // any -> LinearRGB{WP1} = any -> XYZ{WP1} -> LinearRGB{WP1}
            if (typeof(TSource) == typeof(LinearRGBColor))
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