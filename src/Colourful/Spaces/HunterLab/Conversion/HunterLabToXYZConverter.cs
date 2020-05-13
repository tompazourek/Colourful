using System;
using Colourful.Utils;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class HunterLabToXYZConverter : IColorConverter<HunterLabColor, XYZColor>
    {
        private readonly XYZColor _sourceWhitePoint;

        /// <param name="sourceWhitePoint">White point of the source color</param>
        public HunterLabToXYZConverter(in XYZColor sourceWhitePoint)
        {
            _sourceWhitePoint = sourceWhitePoint;
        }

        /// <inheritdoc />
        public XYZColor Convert(in HunterLabColor sourceColor)
        {
            double L = sourceColor.L, a = sourceColor.a, b = sourceColor.b;
            double Xn = _sourceWhitePoint.X, Yn = _sourceWhitePoint.Y, Zn = _sourceWhitePoint.Z;

            var Ka = HunterLabConversionUtils.ComputeKa(in _sourceWhitePoint);
            var Kb = HunterLabConversionUtils.ComputeKb(in _sourceWhitePoint);

            var Y = MathUtils.Pow2(L / 100d) * Yn;
            var X = (a / Ka * Math.Sqrt(Y / Yn) + Y / Yn) * Xn;
            var Z = (b / Kb * Math.Sqrt(Y / Yn) - Y / Yn) * -Zn;

            var targetColor = new XYZColor(in X, in Y, in Z);
            return targetColor;
        }
    }
}