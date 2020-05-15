using System;
using System.Collections.Generic;
using Colourful.Internals;

namespace Colourful
{
    public class ConverterBuilder : IFluentConverterBuilder
    {
        private readonly IConverterFactory _converterFactory;

        public ConverterBuilder(IEnumerable<IConversionStrategy> conversionStrategies)
        {
            if (conversionStrategies == null) throw new ArgumentNullException(nameof(conversionStrategies));
            _converterFactory = new ConverterAbstractFactory(conversionStrategies);
        }
        
        public ConverterBuilder(double[,] lmsTransformationMatrix = null)
        {
            _converterFactory = new ConverterAbstractFactory(ConversionStrategies.GetDefault(lmsTransformationMatrix));
        }

        public ConverterBuilder()
        {
            _converterFactory = new ConverterAbstractFactory(ConversionStrategies.GetDefault());
        }

        public IFluentConverterBuilderFrom<TSource> From<TSource>(IConversionMetadata sourceMetadata) where TSource : IColorSpace
        {
            if (sourceMetadata == null) throw new ArgumentNullException(nameof(sourceMetadata));
            return new FluentFrom<TSource>(_converterFactory, sourceMetadata);
        }

        private class FluentFrom<TSource> : IFluentConverterBuilderFrom<TSource>
            where TSource : IColorSpace
        {
            private readonly IConverterFactory _converterFactory;
            private readonly IConversionMetadata _sourceMetadata;

            public FluentFrom(IConverterFactory converterFactory, IConversionMetadata sourceMetadata)
            {
                _converterFactory = converterFactory ?? throw new ArgumentNullException(nameof(converterFactory));
                _sourceMetadata = sourceMetadata ?? throw new ArgumentNullException(nameof(sourceMetadata));
            }

            public IFluentConverterBuilderFromTo<TSource, TTarget> To<TTarget>(IConversionMetadata targetMetadata) where TTarget : IColorSpace
            {
                if (targetMetadata == null) throw new ArgumentNullException(nameof(targetMetadata));
                return new FluentFromTo<TSource, TTarget>(_converterFactory, _sourceMetadata, targetMetadata);
            }
        }

        private class FluentFromTo<TSource, TTarget> : IFluentConverterBuilderFromTo<TSource, TTarget>
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            private readonly IConverterFactory _converterFactory;
            private readonly IConversionMetadata _sourceMetadata;
            private readonly IConversionMetadata _targetMetadata;

            public FluentFromTo(IConverterFactory converterFactory, IConversionMetadata sourceMetadata, IConversionMetadata targetMetadata)
            {
                _converterFactory = converterFactory ?? throw new ArgumentNullException(nameof(converterFactory));
                _sourceMetadata = sourceMetadata ?? throw new ArgumentNullException(nameof(sourceMetadata));
                _targetMetadata = targetMetadata ?? throw new ArgumentNullException(nameof(targetMetadata));
            }

            public IColorConverter<TSource, TTarget> Build()
                => _converterFactory.CreateConverter<TSource, TTarget>(_sourceMetadata, _targetMetadata);
        }
    }
}