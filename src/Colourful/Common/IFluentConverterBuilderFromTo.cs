namespace Colourful
{
    public interface IFluentConverterBuilderFromTo<TSource, out TTarget>
        where TSource : IColorSpace
        where TTarget : IColorSpace
    {
        IColorConverter<TSource, TTarget> Build();
    }
}