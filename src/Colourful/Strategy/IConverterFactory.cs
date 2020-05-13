namespace Colourful.Strategy
{
    public interface IConverterFactory
    {
        IColorConverter<TSource, TTarget> CreateConverter<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata)
            where TSource : struct
            where TTarget : struct;

        void RegisterStrategy(IConversionStrategy conversionStrategy);
    }
}