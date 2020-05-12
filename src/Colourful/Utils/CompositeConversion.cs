namespace Colourful.Strategy.Rules
{
    public class CompositeConversion<TSource, TIntermediate, TTarget> : IColorConversion<TSource, TTarget>
        where TSource : struct
        where TIntermediate : struct
        where TTarget : struct
    {
        private readonly IColorConversion<TSource, TIntermediate> _firstConversion;
        private readonly IColorConversion<TIntermediate, TTarget> _secondConversion;

        public CompositeConversion(IColorConversion<TSource, TIntermediate> firstConversion, IColorConversion<TIntermediate, TTarget> secondConversion)
        {
            _firstConversion = firstConversion;
            _secondConversion = secondConversion;
        }

        public TTarget Convert(in TSource sourceColor) => _secondConversion.Convert(_firstConversion.Convert(sourceColor));
    }
}