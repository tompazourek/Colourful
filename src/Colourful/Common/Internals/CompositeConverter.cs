namespace Colourful.Internals
{
    public class CompositeConverter<TSource, TIntermediate, TTarget> : IColorConverter<TSource, TTarget>
        where TSource : IColorSpace
        where TIntermediate : IColorSpace
        where TTarget : IColorSpace
    {
        public IColorConverter<TSource, TIntermediate> FirstConverter { get; }
        public IColorConverter<TIntermediate, TTarget> SecondConverter { get; }

        public CompositeConverter(IColorConverter<TSource, TIntermediate> firstConverter, IColorConverter<TIntermediate, TTarget> secondConverter)
        {
            FirstConverter = firstConverter;
            SecondConverter = secondConverter;
        }

        public TTarget Convert(in TSource sourceColor) => SecondConverter.Convert(FirstConverter.Convert(sourceColor));
    }
}