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
using Colourful.Implementation.RGB;

#if (NET40 || NET35)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;
#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor"/> to <see cref="LinearRGBColor"/>.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/> when not set.
    /// </remarks>
    public class XYZToLinearRGBConverter : LinearRGBAndXYZConverterBase, IColorConversion<XYZColor, LinearRGBColor>
    {
        private readonly Matrix _conversionMatrix;

        public XYZToLinearRGBConverter() : this(null)
        {
        }

        public XYZToLinearRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace)
        {
            TargetRGBWorkingSpace = targetRGBWorkingSpace ?? RGBColor.DefaultWorkingSpace;
            _conversionMatrix = GetXYZToRGBMatrix(TargetRGBWorkingSpace);
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; private set; }

        public LinearRGBColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            Vector inputVector = input.Vector;
            Vector uncompandedVector = _conversionMatrix.MultiplyBy(inputVector).Select(x => x.CropRange(0, 1)).ToList();
            LinearRGBColor result = new LinearRGBColor(uncompandedVector, TargetRGBWorkingSpace);
            return result;
        }
        
        #region Overrides

        protected bool Equals(XYZToLinearRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return Equals(TargetRGBWorkingSpace, other.TargetRGBWorkingSpace);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZToLinearRGBConverter) obj);
        }

        public override int GetHashCode()
        {
            return (TargetRGBWorkingSpace != null ? TargetRGBWorkingSpace.GetHashCode() : 0);
        }

        public static bool operator ==(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZToLinearRGBConverter left, XYZToLinearRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}