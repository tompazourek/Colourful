using Colourful.Strategy.Rules;
using Colourful.Strategy;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class RGBConversionStrategy : IConversionStrategy
    {
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory) 
            where TColor : struct
        {
            // only process RGB
            if (typeof(TColor) != typeof(RGBColor)) 
                return null;

            // if same WP, primaries and companding, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata) && EqualRGBPrimaries(in sourceMetadata, in targetMetadata) && EqualCompanding(in sourceMetadata, in targetMetadata))
                return new BypassConverter<RGBColor>() as IColorConverter<TColor, TColor>;

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory) 
            where TSource : struct
            where TTarget : struct
        {
            // RGB{WP1, primaries1} -> LinearRGB{WP1, primaries1}
            if (typeof(TSource) == typeof(RGBColor) && typeof(TTarget) == typeof(LinearRGBColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode) && EqualRGBPrimaries(in sourceNode, in targetNode))
                {
                    return new RGBToLinearRGBConverter(sourceNode.GetCompandingRequired()) as IColorConverter<TSource, TTarget>;
                }
            }
            // LinearRGB{WP1, primaries1} -> RGB{WP1, primaries1}
            else if (typeof(TSource) == typeof(LinearRGBColor) && typeof(TTarget) == typeof(RGBColor))
            {
                if (EqualWhitePoints(in sourceNode, in targetNode) && EqualRGBPrimaries(in sourceNode, in targetNode))
                {
                    return new LinearRGBToRGBConverter(targetNode.GetCompanding()) as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory) 
            where TSource : struct
            where TTarget : struct
        {
            // RGB{WP1, primaries1} -> any = RGB{WP1, primaries1} -> LinearRGB{WP1, primaries1} -> any
            if (typeof(TSource) == typeof(RGBColor))
            {
                var intermediateNode = new ConversionMetadata(sourceNode.GetWhitePointItem(), sourceNode.GetRGBPrimariesItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LinearRGBColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LinearRGBColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, LinearRGBColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
        
        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, in IConverterFactory converterFactory)
            where TSource : struct 
            where TTarget : struct
        {
            // any -> RGB{WP1, primaries1} = any -> LinearRGB{WP1, primaries1} -> RGB{WP1, primaries1}
            if (typeof(TSource) == typeof(RGBColor))
            {
                var intermediateNode = new ConversionMetadata(targetNode.GetWhitePointItem(), targetNode.GetRGBPrimariesItem());
                var firstConversion = converterFactory.CreateConverter<TSource, LinearRGBColor>(in sourceNode, intermediateNode);
                var secondConversion = converterFactory.CreateConverter<LinearRGBColor, TTarget>(intermediateNode, in targetNode);
                return new CompositeConverter<TSource, LinearRGBColor, TTarget>(firstConversion, secondConversion);
            }

            return null;
        }
    }
}