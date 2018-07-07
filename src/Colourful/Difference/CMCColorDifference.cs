using System;
using Colourful.Implementation;

namespace Colourful.Difference
{
    /// <summary>
    /// CMC l:c (1984) color difference
    /// </summary>
    /// <remarks>
    /// Equations: http://www.brucelindbloom.com/index.html?Eqn_DeltaE_CMC.html
    /// </remarks>
    public sealed class CMCColorDifference : IColorDifference<LabColor>
    {
        /// <summary>
        /// Chroma
        /// </summary>
        private readonly double _c;

        /// <summary>
        /// Lightness
        /// </summary>
        private readonly double _l;

        /// <summary>
        /// Constructs with given recommended threshold parameters.
        /// </summary>
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
                    throw new ArgumentOutOfRangeException(nameof(threshold));
            }
        }

        /// <summary>
        /// Constructs with arbitrary threshold parameters.
        /// </summary>
        public CMCColorDifference(double lightness, double chroma)
        {
            _l = lightness;
            _c = chroma;
        }

        /// <inheritdoc />
        public double ComputeDifference(in LabColor x, in LabColor y)
        {
            double L1 = x.L, a1 = x.a, b1 = x.b;
            double L2 = y.L, a2 = y.a, b2 = y.b;

            var dL = L1 - L2;
            var da = a1 - a2;
            var db = b1 - b2;

            var C1 = Math.Sqrt(a1 * a1 + b1 * b1);
            var C2 = Math.Sqrt(a2 * a2 + b2 * b2);
            var dC = C1 - C2;

            var dH_pow2 = da * da + db * db - dC * dC;
            var H1_rad = Math.Atan2(b1, a1);
            var H1 = Angle.NormalizeDegree(Angle.RadianToDegree(H1_rad));

            var C1_pow4 = MathUtils.Pow4(C1);
            var F = Math.Sqrt(C1_pow4 / (C1_pow4 + 1900));

            var T = H1 >= 164 && H1 <= 345
                ? 0.56 + Math.Abs(0.2 * MathUtils.CosDeg(H1 + 168))
                : 0.36 + Math.Abs(0.4 * MathUtils.CosDeg(H1 + 35));

            var SC = 0.0638 * C1 / (1 + 0.0131 * C1) + 0.638;
            var SL = L1 < 16
                ? 0.511
                : 0.040975 * L1 / (1 + 0.01765 * L1);

            var SH = SC * (F * T + 1 - F);

            var dE_1 = dL / (_l * SL);
            var dE_2 = dC / (_c * SC);
            var dE_3_pow2 = dH_pow2 / (SH * SH);

            var dE = Math.Sqrt(dE_1 * dE_1 + dE_2 * dE_2 + dE_3_pow2);
            return dE;
        }
    }

    /// <summary>
    /// Weighting parameters for CMC l:c
    /// </summary>
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