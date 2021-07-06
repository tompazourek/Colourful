namespace Colourful.Internals
{
    /// <summary>
    /// Strategy of a color space that helps to determine the order of conversions.
    /// </summary>
    public interface IConversionStrategy
    {
        /// <summary>
        /// Processes the conversions in case the source and target spaces are the same.
        /// These conversions are attempted 1st (it might result in a bypass conversions <see cref="BypassConverter{TColor}" />).
        /// </summary>
        /// <param name="sourceMetadata">Metadata about the source space.</param>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <param name="converterAbstractFactory">Backreference to the converter factory to facilitate recursion.</param>
        /// <typeparam name="TColor">Both the source and target space.</typeparam>
        /// <returns>If the inputs apply to the strategy, it returns a converter. Otherwise, returns null.</returns>
        IColorConverter<TColor, TColor> TrySame<TColor>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TColor : IColorSpace;

        /// <summary>
        /// Processes the conversions between the source and the target. The intention is to process the direct conversions here.
        /// These conversions are attempted 2nd (after <see cref="TrySame{TColor}" />).
        /// </summary>
        /// <param name="sourceMetadata">Metadata about the source space.</param>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <param name="converterAbstractFactory">Backreference to the converter factory to facilitate recursion.</param>
        /// <typeparam name="TSource">Source space.</typeparam>
        /// <typeparam name="TTarget">Target space.</typeparam>
        /// <returns>If the inputs apply to the strategy, it returns a converter. Otherwise, returns null.</returns>
        IColorConverter<TSource, TTarget> TryConvert<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;

        /// <summary>
        /// Processes the conversions between the source and an unknown target. The intention is to process the conversions through an intermediate space.
        /// These conversions are attempted 3rd (after <see cref="TryConvert{TSource,TTarget}" />).
        /// </summary>
        /// <param name="sourceMetadata">Metadata about the source space.</param>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <param name="converterAbstractFactory">Backreference to the converter factory to facilitate recursion.</param>
        /// <typeparam name="TSource">Source space.</typeparam>
        /// <typeparam name="TTarget">Target space.</typeparam>
        /// <returns>If the inputs apply to the strategy, it returns a converter. Otherwise, returns null.</returns>
        IColorConverter<TSource, TTarget> TryConvertToAnyTarget<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;

        /// <summary>
        /// Processes the conversions between unknown source and the target. The intention is to process the conversions through an intermediate space.
        /// These conversions are attempted 4th (last, after <see cref="TryConvertToAnyTarget{TSource,TTarget}" />).
        /// </summary>
        /// <param name="sourceMetadata">Metadata about the source space.</param>
        /// <param name="targetMetadata">Metadata about the target space.</param>
        /// <param name="converterAbstractFactory">Backreference to the converter factory to facilitate recursion.</param>
        /// <typeparam name="TSource">Source space.</typeparam>
        /// <typeparam name="TTarget">Target space.</typeparam>
        /// <returns>If the inputs apply to the strategy, it returns a converter. Otherwise, returns null.</returns>
        IColorConverter<TSource, TTarget> TryConvertFromAnySource<TSource, TTarget>(in IConversionMetadata sourceMetadata, in IConversionMetadata targetMetadata, in IConverterAbstractFactory converterAbstractFactory)
            where TSource : IColorSpace
            where TTarget : IColorSpace;
    }
}
