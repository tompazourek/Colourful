using Colourful.Internals;

namespace Colourful
{
    public interface IFluentConverterBuilder
    {
        IFluentConverterBuilderFrom<TSource> From<TSource>(IConversionMetadata sourceMetadata) 
            where TSource : IColorSpace;
    }
}