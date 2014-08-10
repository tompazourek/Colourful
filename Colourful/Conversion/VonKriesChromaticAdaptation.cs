#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Colourful.Implementation;
using Colourful.Implementation.Conversion;
using System.Diagnostics.CodeAnalysis;

#if (NET40 || NET35)
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

        public VonKriesChromaticAdaptation(IColorConversion<XYZColor, LMSColor> conversionToLMS, IColorConversion<LMSColor, XYZColor> conversionToXYZ)
        {
            if (conversionToLMS == null) throw new ArgumentNullException("conversionToLMS");
            if (conversionToXYZ == null) throw new ArgumentNullException("conversionToXYZ");

            _conversionToLMS = conversionToLMS;
            _conversionToXYZ = conversionToXYZ;
        }

        /// <summary>
        /// Transforms XYZ color to destination reference white.
        /// </summary>
        public XYZColor Transform(XYZColor sourceColor, XYZColor sourceWhitePoint, XYZColor targetWhitePoint)
        {
            if (sourceColor == null) throw new ArgumentNullException("sourceColor");
            if (sourceWhitePoint == null) throw new ArgumentNullException("sourceWhitePoint");
            if (targetWhitePoint == null) throw new ArgumentNullException("targetWhitePoint");

            if (sourceWhitePoint.Equals(targetWhitePoint))
                return sourceColor;

            LMSColor sourceColorLMS = _conversionToLMS.Convert(sourceColor);
            LMSColor sourceWhitePointLMS = _conversionToLMS.Convert(sourceWhitePoint);
            LMSColor targetWhitePointLMS = _conversionToLMS.Convert(targetWhitePoint);

            Matrix diagonalMatrix = MatrixFactory.CreateDiagonal(targetWhitePointLMS.L / sourceWhitePointLMS.L, targetWhitePointLMS.M / sourceWhitePointLMS.M, targetWhitePointLMS.S / sourceWhitePointLMS.S);

            var targetColorLMS = new LMSColor(diagonalMatrix.MultiplyBy(sourceColorLMS.Vector));
            XYZColor targetColor = _conversionToXYZ.Convert(targetColorLMS);
            return targetColor;
        }
    }
}