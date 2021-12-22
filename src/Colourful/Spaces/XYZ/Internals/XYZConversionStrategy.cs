using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Internals;

/// <inheritdoc />
public class XYZConversionStrategy : IConversionStrategy
{
    /// <inheritdoc />
    public IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TColor : IColorSpace
    {
        // only process XYZ
        if (typeof(TColor) != typeof(XYZColor))
            return null;

        // if equal WP, bypass
        if (EqualWhitePoints(in sourceMetadata, in targetMetadata))
            return new BypassConverter<XYZColor>() as IColorConverter<TColor, TColor>;

        // XYZ{WP1} -> XYZ{WP2} = XYZ{WP1} -> LMS{WP1} -> XYZ{WP2} (WP1 != WP2)
        var intermediateNode = new ConversionMetadata(sourceMetadata.GetWhitePointItem());
        var firstConversion = converterAbstractFactory.CreateConverter<TColor, LMSColor>(in sourceMetadata, intermediateNode);
        var secondConversion = converterAbstractFactory.CreateConverter<LMSColor, TColor>(intermediateNode, in targetMetadata);
        return new CompositeConverter<TColor, LMSColor, TColor>(firstConversion, secondConversion);
    }

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
        => null;

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
        => null;

    /// <inheritdoc />
    public IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
        where TSource : IColorSpace
        where TTarget : IColorSpace
        => null;
}
