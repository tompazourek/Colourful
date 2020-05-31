using System;
using System.Collections.Generic;
using System.Linq;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class ConverterAbstractFactory : IConverterAbstractFactory
    {
        private readonly IConversionStrategy[] _conversionStrategies;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="conversionStrategies">Conversion strategies to use.</param>
        public ConverterAbstractFactory(IEnumerable<IConversionStrategy> conversionStrategies)
        {
            _conversionStrategies = (conversionStrategies ?? throw new ArgumentNullException(nameof(conversionStrategies))).ToArray();
        }

        /// <inheritdoc />
        public IColorConverter<TSource, TTarget> CreateConverter<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata)
            where TSource : IColorSpace
            where TTarget : IColorSpace
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