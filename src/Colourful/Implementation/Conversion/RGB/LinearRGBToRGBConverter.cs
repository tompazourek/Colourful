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
    public class LinearRGBToRGBConverter : IColorConversion<LinearRGBColor, RGBColor>
    {
        public RGBColor Convert(LinearRGBColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            RGBColor result = CompandVector(input.Vector, input.WorkingSpace);
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

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        protected bool Equals(LinearRGBToRGBConverter other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LinearRGBToRGBConverter)obj);
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public static bool operator ==(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LinearRGBToRGBConverter left, LinearRGBToRGBConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
