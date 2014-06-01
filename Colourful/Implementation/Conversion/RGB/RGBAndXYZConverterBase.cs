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
    public abstract class RGBAndXYZConverterBase
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected static Matrix GetRGBToXYZMatrix(IRGBWorkingSpace workingSpace)
        {
            if (workingSpace == null) throw new ArgumentNullException("workingSpace");

            // for more info, see: http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html

            RGBPrimariesChromaticityCoordinates chromaticity = workingSpace.ChromaticityCoordinates;
            double xr = chromaticity.R.x, xg = chromaticity.G.x, xb = chromaticity.B.x,
                   yr = chromaticity.R.y, yg = chromaticity.G.y, yb = chromaticity.B.y;

            double Sr, Sg, Sb;

            double Xr = xr / yr;
            const double Yr = 1;
            double Zr = (1 - xr - yr) / yr;

            double Xg = xg / yg;
            const double Yg = 1;
            double Zg = (1 - xg - yg) / yg;

            double Xb = xb / yb;
            const double Yb = 1;
            double Zb = (1 - xb - yb) / yb;

            var S = new []
                {
                    new[] { Xr, Xg, Xb },
                    new[] { Yr, Yg, Yb },
                    new[] { Zr, Zg, Zb },
                }.Inverse();

            Vector W = workingSpace.WhitePoint.Vector;

            (S.MultiplyBy(W)).AssignVariables(out Sr, out Sg, out Sb);

            var M = new []
                {
                    new[] { Sr * Xr, Sg * Xg, Sb * Xb },
                    new[] { Sr * Yr, Sg * Yg, Sb * Yb },
                    new[] { Sr * Zr, Sg * Zg, Sb * Zb },
                };

            return M;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected static Matrix GetXYZToRGBMatrix(IRGBWorkingSpace workingSpace)
        {
            return GetRGBToXYZMatrix(workingSpace).Inverse();
        }
    }
}