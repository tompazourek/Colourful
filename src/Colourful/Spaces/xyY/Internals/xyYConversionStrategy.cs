using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals;

/// <inheritdoc />
public class xyYConversionStrategy : IConversionStrategy
{
    /// <inheritdoc />
    public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TColor : IColorSpace
    {
        // only process xyY
        if (typeof(TColor) != typeof(xyYColor))
            return null;

        // if equal WP, bypass
        if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
            return new BypassConverter<xyYColor>() as IColorConverter<TColor, TColor>;

        return null;
    }

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        // xyY{WP1} -> XYZ{WP1}
        if (typeof(TSource) == typeof(xyYColor) && typeof(TTarget) == typeof(XYZColor))
        {
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
            {
                return new xyYToXYZConverter() as IColorConverter<TSource, TTarget>;
            }
        }
        // XYZ{WP1} -> xyY{WP1}
        else if (typeof(TSource) == typeof(XYZColor) && typeof(TTarget) == typeof(xyYColor))
        {
            if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
            {
                return new XYZToxyYConverter() as IColorConverter<TSource, TTarget>;
            }
        }

        return null;
    }

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        // xyY{WP1} -> any = xyY{WP1} -> XYZ{WP1} -> any
        if (typeof(TSource) == typeof(xyYColor))
        {
            var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
            var firstConversion = converterAbstractFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
            var secondConversion = converterAbstractFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
            return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
        }

        return null;
    }

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        // any -> xyY{WP1} = any -> XYZ{WP1} -> xyY{WP1}
        if (typeof(TTarget) == typeof(xyYColor))
        {
            var intermediateNode = new ConversionMetadata(targetMetadata.GetWhitePointItem());
            var firstConversion = converterAbstractFactory.CreateConverter<TSource, XYZColor>(in sourceMetadata, intermediateNode);
            var secondConversion = converterAbstractFactory.CreateConverter<XYZColor, TTarget>(intermediateNode, in targetMetadata);
            return new CompositeConverter<TSource, XYZColor, TTarget>(firstConversion, secondConversion);
        }

        return null;
    }
}
