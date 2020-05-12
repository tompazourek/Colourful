using System;
using System.Collections.Generic;
using System.Linq;
using Colourful.Conversion;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Colourful.Strategy
{
    public class ConversionFactory
    {
        public IColorConversion<TSource, TTarget> CreateConversion<TSource, TTarget>(in IConversionMetadata sourceNode, in IConversionMetadata targetNode)
            where TSource : struct
            where TTarget : struct
        {
            var rules = new List<IConversionRule<TSource, TTarget>>();

            // TODO: figure out some way how to register color spaces
            rules.AddRange(HunterLabConversionRules.GetRules<TSource, TTarget>(this));

            var rulesSorted = rules.OrderBy(x => x.Priority).ToList();

            foreach (var conversionRule in rulesSorted)
            {
                if (conversionRule.TryApplyRule(sourceNode, targetNode, out var conversion))
                    return conversion;
            }

            throw new InvalidOperationException("Conversion not possible"); // TODO
        }
    }
}