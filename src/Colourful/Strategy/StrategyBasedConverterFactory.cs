using System;
using System.Collections.Generic;
using System.Linq;

namespace Colourful.Strategy
{
    public class StrategyBasedConverterFactory : IConverterFactory
    {
        private readonly IConversionStrategy[] _conversionStrategies;

        public StrategyBasedConverterFactory(IEnumerable<IConversionStrategy> conversionStrategies)
        {
            if (conversionStrategies == null) throw new ArgumentNullException(nameof(conversionStrategies));

            _conversionStrategies = conversionStrategies.ToArray();
        }

        public IColorConverter<TSource, TTarget> CreateConverter<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata)
            where TSource : struct
            where TTarget : struct
        {
            var conversionStrategies = _conversionStrategies;

            if (typeof(TSource) == typeof(TTarget))
            {
                foreach (var conversionStrategy in conversionStrategies)
                {
                    if (conversionStrategy.TrySame<TSource>(in sourceMetadata, in targetMetadata, this) is IColorConverter<TSource, TTarget> converter)
                        return converter;
                }
            }

            foreach (var conversionStrategy in conversionStrategies)
            {
                if (conversionStrategy.TryConvert<TSource, TTarget>(in sourceMetadata, in targetMetadata, this) is IColorConverter<TSource, TTarget> converter)
                    return converter;
            }

            foreach (var conversionStrategy in conversionStrategies)
            {
                if (conversionStrategy.TryConvertToAnyTarget<TSource, TTarget>(in sourceMetadata, in targetMetadata, this) is IColorConverter<TSource, TTarget> converter)
                    return converter;
            }

            foreach (var conversionStrategy in conversionStrategies)
            {
                if (conversionStrategy.TryConvertFromAnySource<TSource, TTarget>(in sourceMetadata, in targetMetadata, this) is IColorConverter<TSource, TTarget> converter)
                    return converter;
            }

            throw new InvalidOperationException("Conversion not possible according to registered strategies.");
        }
    }
}