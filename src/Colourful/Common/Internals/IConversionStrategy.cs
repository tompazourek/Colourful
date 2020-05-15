namespace Colourful.Internals
{
    public interface IConversionStrategy
    {
        IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TColor : IColorSpace;

        IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;

        IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;
        
        IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterFactory converterFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;
    }
}