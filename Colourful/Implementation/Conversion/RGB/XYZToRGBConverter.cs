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
    /// Converts from <see cref="XYZColor"/> to <see cref="RGBColor"/>.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/> when not set.
    /// </remarks>
    public class XYZToRGBConverter : RGBAndXYZConverterBase, IColorConversion<XYZColor, RGBColor>
    {
        private readonly Matrix _conversionMatrix;

        public XYZToRGBConverter() : this(null)
        {
        }

        public XYZToRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace)
        {
            TargetRGBWorkingSpace = targetRGBWorkingSpace ?? RGBColor.DefaultWorkingSpace;
            _conversionMatrix = GetXYZToRGBMatrix(TargetRGBWorkingSpace);
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; private set; }

        public RGBColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            Vector inputVector = input.Vector;
            Vector uncompandedVector = _conversionMatrix.MultiplyBy(inputVector);
            RGBColor result1 = CompandVector(uncompandedVector, TargetRGBWorkingSpace);
            RGBColor result = result1;
            return result;
        }

        /// <summary>
        /// Applying the working space companding function (<see cref="IRGBWorkingSpace.Companding"/>) to uncompanded vector.
        /// </summary>
        private static RGBColor CompandVector(Vector uncompandedVector, IRGBWorkingSpace workingSpace)
        {
            ICompanding companding = workingSpace.Companding;
            Vector compandedVector = uncompandedVector.Select(companding.Companding).ToList();
            double R, G, B;
            compandedVector.AssignVariables(out R, out G, out B);

            R = R.CropRange(0, 1);
            G = G.CropRange(0, 1);
            B = B.CropRange(0, 1);

            var result = new RGBColor(R, G, B, workingSpace);
            return result;
        }

        #region Overrides

        protected bool Equals(XYZToRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return Equals(TargetRGBWorkingSpace, other.TargetRGBWorkingSpace);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XYZToRGBConverter) obj);
        }

        public override int GetHashCode()
        {
            return (TargetRGBWorkingSpace != null ? TargetRGBWorkingSpace.GetHashCode() : 0);
        }

        public static bool operator ==(XYZToRGBConverter left, XYZToRGBConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(XYZToRGBConverter left, XYZToRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}