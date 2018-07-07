using System;
using Colourful.Implementation;
using Colourful.Implementation.Conversion;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Conversion
{
    /// <summary>
    /// Basic implementation of the von Kries chromatic adaptation model
    /// </summary>
    /// <remarks>
    /// Transformation described here:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public sealed class VonKriesChromaticAdaptation : IChromaticAdaptation
    {
        private readonly IColorConversion<XYZColor, LMSColor> _conversionToLMS;
        private readonly IColorConversion<LMSColor, XYZColor> _conversionToXYZ;
        private Matrix _cachedDiagonalMatrix;

        private XYZColor _lastSourceWhitePoint;
        private XYZColor _lastTargetWhitePoint;

        /// <summary>
        /// Constructs von Kries chromatic adaptation using default <see cref="XYZAndLMSConverter" />
        /// </summary>
        public VonKriesChromaticAdaptation() : this(new XYZAndLMSConverter())
        {
        }

        /// <summary>
        /// Transformation matrix used for the conversion (definition of the cone response domain).
        /// <see cref="LMSTransformationMatrix" />
        /// </summary>
        public VonKriesChromaticAdaptation(Matrix transformationMatrix) : this(new XYZAndLMSConverter(transformationMatrix))
        {
        }

        private VonKriesChromaticAdaptation(XYZAndLMSConverter converter) : this(converter, converter)
        {
        }

        /// <summary>
        /// Constructs von Kries chromatic adaptation using given converters
        /// </summary>
        public VonKriesChromaticAdaptation(IColorConversion<XYZColor, LMSColor> conversionToLMS, IColorConversion<LMSColor, XYZColor> conversionToXYZ)
        {
            _conversionToLMS = conversionToLMS ?? throw new ArgumentNullException(nameof(conversionToLMS));
            _conversionToXYZ = conversionToXYZ ?? throw new ArgumentNullException(nameof(conversionToXYZ));
        }

        /// <summary>
        /// Transforms XYZ color to destination reference white.
        /// </summary>
        public XYZColor Transform(in XYZColor sourceColor, in XYZColor sourceWhitePoint, in XYZColor targetWhitePoint)
        {
            if (sourceWhitePoint.Equals(targetWhitePoint))
                return sourceColor;

            var sourceColorLMS = _conversionToLMS.Convert(sourceColor);

            if (sourceWhitePoint != _lastSourceWhitePoint || targetWhitePoint != _lastTargetWhitePoint)
            {
                var sourceWhitePointLMS = _conversionToLMS.Convert(sourceWhitePoint);
                var targetWhitePointLMS = _conversionToLMS.Convert(targetWhitePoint);

                _cachedDiagonalMatrix = MatrixFactory.CreateDiagonal(targetWhitePointLMS.L / sourceWhitePointLMS.L, targetWhitePointLMS.M / sourceWhitePointLMS.M, targetWhitePointLMS.S / sourceWhitePointLMS.S);
                _lastSourceWhitePoint = sourceWhitePoint;
                _lastTargetWhitePoint = targetWhitePoint;
            }

            var targetColorLMS = new LMSColor(_cachedDiagonalMatrix.MultiplyBy(sourceColorLMS.Vector));
            var targetColor = _conversionToXYZ.Convert(targetColorLMS);
            return targetColor;
        }
    }
}