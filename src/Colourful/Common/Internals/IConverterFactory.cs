namespace Colourful.Internals
{
    public interface IConverterFactory
    {
        IColorConverter<TSource, TTarget> CreateConverter<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata)
            where TSource : IColorSpace
            where TTarget : IColorSpace;
    }
}