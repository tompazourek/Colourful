using static Colourful.Strategy.ConversionRulePriorities;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Strategy.Rules
{
    public class Intermediate_UseTargetWhitePoint_UseTargetRGBPrimaries<TSource, TIntermediate, TTarget> : IConversionRule<TSource, TTarget>
        where TSource : struct
        where TIntermediate : struct
        where TTarget : struct
    {
        private readonly ConversionFactory _conversionFactory;

        public Intermediate_UseTargetWhitePoint_UseTargetRGBPrimaries(ConversionFactory conversionFactory)
        {
            _conversionFactory = conversionFactory;
        }

        public virtual int Priority => IntermediateToAny;

        public virtual bool TryApplyRule(in IConversionMetadata sourceNode, in IConversionMetadata targetNode, out IColorConversion<TSource, TTarget> conversion)
        {
            var intermediateNode = (IConversionMetadata)new ConversionMetadata(CreateWhitePoint(targetNode.GetWhitePoint()), CreateRGBPrimaries(targetNode.GetRGBPrimaries()));
            var firstConversion = _conversionFactory.CreateConversion<TSource, TIntermediate>(in sourceNode, in intermediateNode);
            var secondConversion = _conversionFactory.CreateConversion<TIntermediate, TTarget>(in intermediateNode, in targetNode);
            var compositeConversion = new CompositeConversion<TSource, TIntermediate, TTarget>(firstConversion, secondConversion);
            conversion = compositeConversion;
            return true;
        }
    }
}