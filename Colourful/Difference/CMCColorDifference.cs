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
using Colourful.Implementation;

namespace Colourful.Difference
{
    /// <summary>
    /// CMC l:c (1984) color difference
    /// </summary>
    /// <remarks>
    /// Equations: http://www.brucelindbloom.com/index.html?Eqn_DeltaE_CMC.html
    /// </remarks>
    public class CMCColorDifference : IColorDifference<LabColor>
    {
        /// <summary>
        /// Chroma
        /// </summary>
        private readonly double _c;

        /// <summary>
        /// Lightness
        /// </summary>
        private readonly double _l;

        public CMCColorDifference(CMCColorDifferenceThreshold threshold)
        {
            switch (threshold)
            {
                case CMCColorDifferenceThreshold.Acceptability:
                    _l = 2;
                    _c = 1;
                    break;
                case CMCColorDifferenceThreshold.Imperceptibility:
                    _l = 1;
                    _c = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("threshold");
            }
        }

        public CMCColorDifference(double lightness, double chroma)
        {
            _l = lightness;
            _c = chroma;
        }

        public double ComputeDifference(LabColor x, LabColor y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            double L1 = x.L, a1 = x.a, b1 = x.b;
            double L2 = y.L, a2 = y.a, b2 = y.b;

            double dL = L1 - L2;
            double da = a1 - a2;
            double db = b1 - b2;

            double C1 = Math.Sqrt(a1 * a1 + b1 * b1);
            double C2 = Math.Sqrt(a2 * a2 + b2 * b2);
            double dC = C1 - C2;

            double dH = Math.Sqrt(da * da + db * db - dC * dC);
            double H1_rad = Math.Atan2(b1, a1);
            double H1 = Angle.RadianToDegree(H1_rad);

            if (H1 < 0)
                H1 += 360;
            else if (H1 > 360)
                H1 -= 360;

            double C1_pow4 = (C1 * C1 * C1 * C1);
            double F = Math.Sqrt(C1_pow4 / (C1_pow4 + 1900));

            double T = H1 >= 164 && H1 <= 345
                ? 0.56 + Math.Abs(0.2 * CosDeg(H1 + 168))
                : 0.36 + Math.Abs(0.4 * CosDeg(H1 + 35));

            double SC = (0.0638 * C1) / (1 + 0.0131 * C1) + 0.638;
            double SL = L1 < 16
                ? 0.511
                : (0.040975 * L1) / (1 + 0.01765 * L1);
            double SH = SC * (F * T + 1 - F);

            double dE_1 = dL / (_l * SL);
            double dE_2 = dC / (_c * SC);
            double dE_3 = dH / SH;

            double dE = Math.Sqrt(dE_1 * dE_1 + dE_2 * dE_2 + dE_3 * dE_3);
            return dE;
        }

        /// <summary>
        /// Compute cosine of angle in degrees
        /// </summary>
        /// <param name="x">Given angle</param>
        /// <returns></returns>
        private static double CosDeg(double x)
        {
            double x_rad = Angle.DegreeToRadian(x);
            double y = Math.Cos(x_rad);
            return y;
        }
    }

    public enum CMCColorDifferenceThreshold
    {
        /// <summary>
        /// 2:1 (l:c)
        /// </summary>
        Acceptability,

        /// <summary>
        /// 1:1 (l:c)
        /// </summary>
        Imperceptibility
    }
}