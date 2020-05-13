namespace Colourful.Strategy.Rules
{
    public class CompositeConverter<TSource, TIntermediate, TTarget> : IColorConverter<TSource, TTarget>
        where TSource : struct
        where TIntermediate : struct
        where TTarget : struct
    {
        private readonly IColorConverter<TSource, TIntermediate> _firstConverter;
        private readonly IColorConverter<TIntermediate, TTarget> _secondConverter;

        public CompositeConverter(IColorConverter<TSource, TIntermediate> firstConverter, IColorConverter<TIntermediate, TTarget> secondConverter)
        {
            _firstConverter = firstConverter;
            _secondConverter = secondConverter;
        }

        public TTarget Convert(in TSource sourceColor) => _secondConverter.Convert(_firstConverter.Convert(sourceColor));
    }
}