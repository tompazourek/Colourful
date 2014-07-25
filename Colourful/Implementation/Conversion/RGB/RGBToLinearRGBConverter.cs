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
using System.Diagnostics.CodeAnalysis;
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
    public class RGBToLinearRGBConverter : IColorConversion<RGBColor, LinearRGBColor>
    {
        public LinearRGBColor Convert(RGBColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            Vector uncompandedVector = UncompandVector(input);
            var converted = new LinearRGBColor(uncompandedVector, input.WorkingSpace);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding"/>) to RGB vector.
        /// </summary>
        private static Vector UncompandVector(RGBColor rgbColor)
        {
            ICompanding inverseCompanding = rgbColor.WorkingSpace.Companding;
            Vector compandedVector = rgbColor.Vector;
            Vector uncompandedVector = compandedVector.Select(x => inverseCompanding.InverseCompanding(x).CropRange(0, 1)).ToList();
            return uncompandedVector;
        }

        #region Overrides

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected bool Equals(RGBToLinearRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBToLinearRGBConverter)obj);
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public static bool operator ==(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RGBToLinearRGBConverter left, RGBToLinearRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
