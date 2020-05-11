using System;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class XYZToHunterLabConversion : IColorConversion<XYZColor, HunterLabColor>
    {
        private readonly XYZColor _targetWhitePoint;

        /// <param name="targetWhitePoint">White point of the target color</param>
        public XYZToHunterLabConversion(in XYZColor targetWhitePoint)
        {
            _targetWhitePoint = targetWhitePoint;
        }

        /// <inheritdoc />
        public HunterLabColor Convert(in XYZColor sourceColor)
        {
            // conversion algorithm described here: http://en.wikipedia.org/wiki/Lab_color_space#Hunter_Lab
            double X = sourceColor.X, Y = sourceColor.Y, Z = sourceColor.Z;
            double Xn = _targetWhitePoint.X, Yn = _targetWhitePoint.Y, Zn = _targetWhitePoint.Z;

            var Ka = HunterLabConversionUtils.ComputeKa(in _targetWhitePoint);
            var Kb = HunterLabConversionUtils.ComputeKb(in _targetWhitePoint);

            var L = 100 * Math.Sqrt(Y / Yn);
            var a = Ka * ((X / Xn - Y / Yn) / Math.Sqrt(Y / Yn));
            var b = Kb * ((Y / Yn - Z / Zn) / Math.Sqrt(Y / Yn));

            if (double.IsNaN(a))
                a = 0;

            if (double.IsNaN(b))
                b = 0;

            var targetColor = new HunterLabColor(in L, in a, in b);
            return targetColor;
        }
    }
}