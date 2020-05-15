using Colourful.Internals;

namespace Colourful
{
    public interface IFluentConverterBuilderFrom<TSource>
        where TSource : IColorSpace
    {
        IFluentConverterBuilderFromTo<TSource, TTarget> To<TTarget>(IConversionMetadata targetMetadata)
            where TTarget : IColorSpace;
    }
}