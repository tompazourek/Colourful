using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class LMSConversionStrategy : IConversionStrategy
    {
        private readonly double[,] _transformationMatrix;

        /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />).</param>
        public LMSConversionStrategy(double[,] transformationMatrix)
        {
            _transformationMatrix = transformationMatrix;
        }

        /// <inheritdoc />
        public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace
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

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
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

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            if (typeof(TSource) == typeof(LMSColor))
            {
                // LMS{WP1} -> any{WP1} = LMS{WP1} -> XYZ{WP1} -> any{WP1}
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                    var firstConversion = converterAbstractFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterAbstractFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
                }
                // LMS{WP1} -> any{WP2} = LMS{WP1} -> LMS{WP2} -> any{WP2} (WP1 != WP2)
                else
                {
                    var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                    var firstConversion = converterAbstractFactory.CreateConverter<TSource, LMSColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterAbstractFactory.CreateConverter<LMSColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, LMSColor, TTarget>(firstConversion, secondConversion);
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            if (typeof(TTarget) == typeof(LMSColor))
            {
                // any{WP1} -> LMS{WP1} = any{WP1} -> XYZ{WP1} -> LMS{WP1}
                if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
                {
                    var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
                    var firstConversion = converterAbstractFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterAbstractFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
                }
                // any{WP1} -> LMS{WP2} = any{WP1} -> LMS{WP1} -> LMS{WP2} (WP1 != WP2)
                else
                {
                    var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
                    var firstConversion = converterAbstractFactory.CreateConverter<TSource, LMSColor>(in sourceMetadata, intermediateNode);
                    var secondConversion = converterAbstractFactory.CreateConverter<LMSColor, TTarget>(intermediateNode, in targetMetadata);
                    return new CompositeConverter<TSource, LMSColor, TTarget>(firstConversion, secondConversion);
                }
            }

            return null;
        }
    }
}