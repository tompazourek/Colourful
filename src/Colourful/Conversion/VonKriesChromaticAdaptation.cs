using System;
using Colourful.Implementation;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Colourful.Implementation.Conversion;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful.Conversion
{
    /// <summary>
    /// Basic implementation of the von Kries chromatic adaptation model
    /// </summary>
    /// <remarks>
    /// Transformation described here:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class VonKriesChromaticAdaptation : IChromaticAdaptation
    {
        private readonly IColorConversion<XYZColor, LMSColor> _conversionToLMS;
        private readonly IColorConversion<LMSColor, XYZColor> _conversionToXYZ;

        private XYZColor _lastSourceWhitePoint;
        private XYZColor _lastTargetWhitePoint;
        private Matrix _cachedDiagonalMatrix;

        /// <summary>
        /// Constructs von Kries chromatic adaptation using default <see cref="XYZAndLMSConverter"/>
        /// </summary>
        public VonKriesChromaticAdaptation() : this(new XYZAndLMSConverter())
        {
        }

        /// <summary>
        /// Transformation matrix used for the conversion (definition of the cone response domain).
        /// <see cref="LMSTransformationMatrix"/>
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
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
            if (conversionToLMS == null) throw new ArgumentNullException(nameof(conversionToLMS));
            if (conversionToXYZ == null) throw new ArgumentNullException(nameof(conversionToXYZ));

            _conversionToLMS = conversionToLMS;
            _conversionToXYZ = conversionToXYZ;
        }

        /// <summary>
        /// Transforms XYZ color to destination reference white.
        /// </summary>
        public XYZColor Transform(XYZColor sourceColor, XYZColor sourceWhitePoint, XYZColor targetWhitePoint)
        {
            if (sourceColor == null) throw new ArgumentNullException(nameof(sourceColor));
            if (sourceWhitePoint == null) throw new ArgumentNullException(nameof(sourceWhitePoint));
            if (targetWhitePoint == null) throw new ArgumentNullException(nameof(targetWhitePoint));

            if (sourceWhitePoint.Equals(targetWhitePoint))
                return sourceColor;

            var sourceColorLMS = _conversionToLMS.Convert(sourceColor);

            if (sourceWhitePoint != _lastSourceWhitePoint || targetWhitePoint != _lastTargetWhitePoint)
            {
                var sourceWhitePointLMS = _conversionToLMS.Convert(sourceWhitePoint);
                var targetWhitePointLMS = _conversionToLMS.Convert(targetWhitePoint);

                _cachedDiagonalMatrix = MatrixFactory.CreateDiagonal(targetWhitePointLMS.L/sourceWhitePointLMS.L, targetWhitePointLMS.M/sourceWhitePointLMS.M, targetWhitePointLMS.S/sourceWhitePointLMS.S);
                _lastSourceWhitePoint = sourceWhitePoint;
                _lastTargetWhitePoint = targetWhitePoint;
            }

            var targetColorLMS = new LMSColor(_cachedDiagonalMatrix.MultiplyBy(sourceColorLMS.Vector));
            var targetColor = _conversionToXYZ.Convert(targetColorLMS);
            return targetColor;
        }
    }
}