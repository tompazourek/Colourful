using System;
using System.Collections.Generic;
using Colourful.Internals;

namespace Colourful
{
    /// <summary>
    /// Primary API to be used to create color converters. It provides a fluent interface.
    /// </summary>
    public class ConverterBuilder : IFluentConverterBuilder
    {
        private readonly IConverterAbstractFactory _converterAbstractFactory;

        /// <summary>
        /// If you want to customize the conversion process, provide your own list of conversion strategies.
        /// Otherwise, use the parameterless constructor that uses <see cref="ConversionStrategies.GetDefault" />.
        /// </summary>
        public ConverterBuilder(IEnumerable<IConversionStrategy> conversionStrategies) => _converterAbstractFactory = new ConverterAbstractFactory(conversionStrategies ?? throw new ArgumentNullException(nameof(conversionStrategies)));

        /// <summary>
        /// Creates a builder with all the defaults (all built-in strategies).
        /// </summary>
        /// <param name="lmsTransformationMatrix">Optionally pick LMS transformation matrix (<see cref="LMSTransformationMatrix" />) used for LMS-XYZ conversion and chromatic adaptation for color spaces with different white points. If empty, <see cref="LMSTransformationMatrix.Bradford" /> will be used.</param>
        public ConverterBuilder(double[,] lmsTransformationMatrix = null) : this(ConversionStrategies.GetDefault(lmsTransformationMatrix))
        {
        }

        /// <summary>
        /// Creates a builder with all the defaults (all built-in strategies with default settings).
        /// </summary>
        public ConverterBuilder() : this(ConversionStrategies.GetDefault())
        {
        }

        /// <inheritdoc />
        public IFluentConverterBuilderFrom<TSource> From<TSource>(IConversionMetadata sourceMetadata) where TSource : IColorSpace
            => new FluentFrom<TSource>(_converterAbstractFactory, sourceMetadata ?? throw new ArgumentNullException(nameof(sourceMetadata)));

        private class FluentFrom<TSource> : IFluentConverterBuilderFrom<TSource>
            where TSource : IColorSpace
        {
            private readonly IConverterAbstractFactory _converterAbstractFactory;
            private readonly IConversionMetadata _sourceMetadata;

            public FluentFrom(IConverterAbstractFactory converterAbstractFactory, IConversionMetadata sourceMetadata)
            {
                _converterAbstractFactory = converterAbstractFactory ?? throw new ArgumentNullException(nameof(converterAbstractFactory));
                _sourceMetadata = sourceMetadata ?? throw new ArgumentNullException(nameof(sourceMetadata));
            }

            public IFluentConverterBuilderFromTo<TSource, TTarget> To<TTarget>(IConversionMetadata targetMetadata) where TTarget : IColorSpace
                => new FluentFromTo<TSource, TTarget>(_converterAbstractFactory, _sourceMetadata, targetMetadata ?? throw new ArgumentNullException(nameof(targetMetadata)));
        }

        private class FluentFromTo<TSource, TTarget> : IFluentConverterBuilderFromTo<TSource, TTarget>
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            private readonly IConverterAbstractFactory _converterAbstractFactory;
            private readonly IConversionMetadata _sourceMetadata;
            private readonly IConversionMetadata _targetMetadata;

            public FluentFromTo(IConverterAbstractFactory converterAbstractFactory, IConversionMetadata sourceMetadata, IConversionMetadata targetMetadata)
            {
                _converterAbstractFactory = converterAbstractFactory ?? throw new ArgumentNullException(nameof(converterAbstractFactory));
                _sourceMetadata = sourceMetadata ?? throw new ArgumentNullException(nameof(sourceMetadata));
                _targetMetadata = targetMetadata ?? throw new ArgumentNullException(nameof(targetMetadata));
            }

            public IColorConverter<TSource, TTarget> Build()
                => _converterAbstractFactory.CreateConverter<TSource, TTarget>(_sourceMetadata, _targetMetadata);
        }
    }
}
