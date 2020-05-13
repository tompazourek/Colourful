using Colourful.Strategy;
using Colourful.Utils;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Conversion
{
    public class LMSConversionStrategy : IConversionStrategy
    {
        private readonly double[,] _transformationMatrix;

        public LMSConversionStrategy(double[,] transformationMatrix)
        {
            _transformationMatrix = transformationMatrix;
        }

        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : struct
        {
            // only process LMS
            if (typeof(TColor) != typeof(LMSColor))
                return null;

            // if equal WP, bypass
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                return new BypassConverter<LMSColor>() as IColorConverter<TColor, TColor>;

            // LMS{WP1} -> LMS{WP2} (WP1 != WP2)
            var sourceWhitePointXYZ = sourceMetadata.GetWhitePointRequired();
            var targetWhitePointXYZ = targetMetadata.GetWhitePointRequired();
            var whitePointConversion = new XYZToLMSConverter(in _transformationMatrix);
            var sourceWhitePointLMS = whitePointConversion.Convert(in sourceWhitePointXYZ);
            var targetWhitePointLMS = whitePointConversion.Convert(in targetWhitePointXYZ);
            return new VonKriesChromaticAdaptation(in sourceWhitePointLMS, in targetWhitePointLMS) as IColorConverter<TColor, TColor>;
        }

        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            // LMS{WP1} -> XYZ{WP1}
            if (typeof(TSource) == typeof(LMSColor) && typeof(TTarget) == typeof(XYZColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new LMSToXYZConverter(in _transformationMatrix) as IColorConverter<TSource, TTarget>;
                }
            }
            // XYZ{WP1} -> LMS{WP1}
            else if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(LMSColor))
            {
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    return new XYZToLMSConverter(in _transformationMatrix) as IColorConverter<TSource, TTarget>;
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            if (typeof(TSource) == typeof(LMSColor))
            {
                // LMS{WP1} -> any{WP1} = LMS{WP1} -> XYZ{WP1} -> any{WP1}
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                    var firstConversion = converterFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
                }
                // LMS{WP1} -> any{WP2} = LMS{WP1} -> LMS{WP2} -> any{WP2} (WP1 != WP2)
                else
                {
                    var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                    var firstConversion = converterFactory.CreateConverter<TSource, LMSColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterFactory.CreateConverter<LMSColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, LMSColor, TTarget>(firstConversion, secondConversion);
                }
            }

            return null;
        }

        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : struct
            where TTarget : struct
        {
            if (typeof(TTarget) == typeof(LMSColor))
            {
                // any{WP1} -> LMS{WP1} = any{WP1} -> XYZ{WP1} -> LMS{WP1}
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                    var firstConversion = converterFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
                }
                // any{WP1} -> LMS{WP2} = any{WP1} -> LMS{WP1} -> LMS{WP2} (WP1 != WP2)
                else
                {
                    var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                    var firstConversion = converterFactory.CreateConverter<TSource, LMSColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterFactory.CreateConverter<LMSColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, LMSColor, TTarget>(firstConversion, secondConversion);
                }
            }

            return null;
        }
    }
}