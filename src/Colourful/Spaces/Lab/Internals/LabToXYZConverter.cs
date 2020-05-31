using static Colourful.Internals.CIEConstants;
using static Colourful.Internals.MathUtils;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class LabToXYZConverter : IColorConverter<LabColor, XYZColor>
    {
        private readonly XYZColor _sourceWhitePoint;

        /// <param name="sourceWhitePoint">White point of the source color.</param>
        public LabToXYZConverter(in XYZColor sourceWhitePoint)
        {
            _sourceWhitePoint = sourceWhitePoint;
        }

        /// <inheritdoc />
        public XYZColor Convert(in LabColor sourceColor)
        {
            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_Lab_to_XYZ.html
            double L = sourceColor.L, a = sourceColor.a, b = sourceColor.b;
            var fy = (L + 16) / 116d;
            var fx = a / 500d + fy;
            var fz = fy - b / 200d;

            var fx3 = Pow3(fx);
            var fz3 = Pow3(fz);

            var xr = fx3 > Epsilon ? fx3 : (116 * fx - 16) / Kappa;
            var yr = L > Kappa * Epsilon ? Pow3((L + 16) / 116d) : L / Kappa;
            var zr = fz3 > Epsilon ? fz3 : (116 * fz - 16) / Kappa;

            double Xr = _sourceWhitePoint.X, Yr = _sourceWhitePoint.Y, Zr = _sourceWhitePoint.Z;

            var X = xr * Xr;
            var Y = yr * Yr;
            var Z = zr * Zr;

            var targetColor = new XYZColor(in X, in Y, in Z);
            return targetColor;
        }
    }
}