using System;
using System.Collections.Generic;
using System.Linq;
using Colourful.Conversion;
using Colourful.Strategy.Rules;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Colourful.Strategy
{
    public class ConverterFactory
    {
        private readonly List<IConversionRule> _rulesSorted;

        public ConverterFactory()
        {
            // TODO: make extensible
            var rules = new List<IConversionRule>();
            rules.AddRange(HunterLabConversionRules.GetRules());
            rules.AddRange(LabConversionRules.GetRules());
            rules.AddRange(LChabConversionRules.GetRules());
            rules.AddRange(LChuvConversionRules.GetRules());
            rules.AddRange(LinearRGBConversionRules.GetRules());
            rules.AddRange(LMSConversionRules.GetRules(LMSTransformationMatrix.Bradford));
            rules.AddRange(LuvConversionRules.GetRules());
            rules.AddRange(RGBConversionRules.GetRules());
            rules.AddRange(xyYConversionRules.GetRules());
            rules.AddRange(XYZConversionRules.GetRules());
            _rulesSorted = rules.OrderBy(x => x.Priority).ToList();
        }

        public IColorConversion<TSource, TTarget> CreateConverter<TSource, TTarget>
        (
            // TODO: better config of additional metadata (RGB working spaces, illuminants, etc.)
            KeyValuePair<string, object>[] sourceAdditionalMetadata = null,
            KeyValuePair<string, object>[] targetAdditionalMetadata = null
        )
            where TSource : struct
            where TTarget : struct
        {
            var sourceItems = new [] { ConversionRuleUtils.CreateColorType<TSource>() }.Concat(sourceAdditionalMetadata ?? Enumerable.Empty<KeyValuePair<string, object>>());
            var targetItems = new [] { ConversionRuleUtils.CreateColorType<TTarget>() }.Concat(targetAdditionalMetadata ?? Enumerable.Empty<KeyValuePair<string, object>>());
            var sourceNode = new ConversionMetadata(sourceItems.ToArray());
            var targetNode = new ConversionMetadata(targetItems.ToArray());
            var conversionSequence = GetConversionSequence(sourceNode, targetNode);
            var conversion = CreateConversionFromSequence<TSource, TTarget>(conversionSequence);
            return conversion;
        }

        private static IColorConversion<TSource, TTarget> CreateConversionFromSequence<TSource, TTarget>(IEnumerable<object> conversionSequence)
            where TSource : struct
            where TTarget : struct
        {
            var currentType = typeof(TSource);
            foreach (var conversion in conversionSequence)
            {
                var conversionType = conversion.GetType();
                // TODO?
            }
            
            if (currentType != typeof(TTarget))
                throw new InvalidOperationException("Something went wrong, the conversion result doesn't match."); // TODO: make better exception (shouldn't happen anyway)

            throw new NotImplementedException();
        }

        internal IEnumerable<object> GetConversionSequence(IConversionMetadata sourceNode, IConversionMetadata targetNode)
        {
            var chain = new ConversionChain(sourceNode, targetNode);

            restart:

            foreach (var nodePair in chain.GetPairs())
            {
                foreach (var rule in _rulesSorted)
                {
                    if (!rule.TryApplyRule(nodePair.SourceNode, nodePair.TargetNode, out var replacementNodes, out var conversion))
                        continue;

                    nodePair.ReplaceInChain(replacementNodes);

                    if (conversion != null)
                        yield return conversion;

                    goto restart;
                }
            }

            if (chain.Count != 1)
                throw new InvalidOperationException("Conversion cannot be performed!"); // TODO: nicer exception
        }
    }
}