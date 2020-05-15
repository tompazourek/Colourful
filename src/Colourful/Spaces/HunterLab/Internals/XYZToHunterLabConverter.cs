using static System.Double;
using static System.Math;
using static Colourful.Internals.HunterLabConversionUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class XYZToHunterLabConverter : IColorConverter<XYZColor, HunterLabColor>
    {
        private readonly XYZColor _targetWhitePoint;

        /// <param name="targetWhitePoint">White point of the target color</param>
        public XYZToHunterLabConverter(in XYZColor targetWhitePoint)
        {
            _targetWhitePoint = targetWhitePoint;
        }

        /// <inheritdoc />
        public HunterLabColor Convert(in XYZColor sourceColor)
        {
            // conversion algorithm described here: http://en.wikipedia.org/wiki/Lab_color_space#Hunter_Lab
            double X = sourceColor.X, Y = sourceColor.Y, Z = sourceColor.Z;
            double Xn = _targetWhitePoint.X, Yn = _targetWhitePoint.Y, Zn = _targetWhitePoint.Z;

            var Ka = ComputeKa(in _targetWhitePoint);
            var Kb = ComputeKb(in _targetWhitePoint);

            var L = 100 * Sqrt(Y / Yn);
            var a = Ka * ((X / Xn - Y / Yn) / Sqrt(Y / Yn));
            var b = Kb * ((Y / Yn - Z / Zn) / Sqrt(Y / Yn));

            if (IsNaN(a))
                a = 0;

            if (IsNaN(b))
                b = 0;

            var targetColor = new HunterLabColor(in L, in a, in b);
            return targetColor;
        }
    }
}