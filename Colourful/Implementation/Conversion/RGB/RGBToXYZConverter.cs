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
using System.Threading.Tasks;
using Colourful.Implementation.RGB;
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="RGBColor"/> to <see cref="XYZColor"/>.
    /// </summary>
    public class RGBToXYZConverter : RGBAndXYZConverterBase, IColorConversion<RGBColor, XYZColor>
    {
        private readonly Matrix _conversionMatrix;

        /// <param name="sourceRGBWorkingSpace">Source RGB working space</param>
        public RGBToXYZConverter(IRGBWorkingSpace sourceRGBWorkingSpace)
        {
            SourceRGBWorkingSpace = sourceRGBWorkingSpace;
            _conversionMatrix = GetRGBToXYZMatrix(SourceRGBWorkingSpace);
        }

        /// <summary>
        /// Source RGB working space
        /// </summary>
        public IRGBWorkingSpace SourceRGBWorkingSpace { get; private set; }

        public XYZColor Convert(RGBColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            if (!Equals(input.WorkingSpace, SourceRGBWorkingSpace))
                throw new InvalidOperationException("Working space of input RGB color must be equal to converter source RGB working space.");

            Vector rgb = UncompandVector(input);
            Vector xyz = _conversionMatrix.MultiplyBy(rgb);

            double x, y, z;
            xyz.AssignVariables(out x, out y, out z);

            var converted = new XYZColor(x, y, z);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding"/>) to RGB vector.
        /// </summary>
        private static Vector UncompandVector(RGBColor rgbColor)
        {
            ICompanding inverseCompanding = rgbColor.WorkingSpace.Companding;
            Vector compandedVector = rgbColor.Vector;
            Vector uncompandedVector = compandedVector.Select(inverseCompanding.InverseCompanding).ToList();
            return uncompandedVector;
        }

        #region Overrides

        protected bool Equals(RGBToXYZConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return Equals(SourceRGBWorkingSpace, other.SourceRGBWorkingSpace);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBToXYZConverter) obj);
        }

        public override int GetHashCode()
        {
            return (SourceRGBWorkingSpace != null ? SourceRGBWorkingSpace.GetHashCode() : 0);
        }

        public static bool operator ==(RGBToXYZConverter left, RGBToXYZConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RGBToXYZConverter left, RGBToXYZConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}