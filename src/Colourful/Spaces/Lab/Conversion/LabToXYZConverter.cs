namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LabToXYZConverter : IColorConverter<LabColor, XYZColor>
    {
        private readonly XYZColor _sourceWhitePoint;

        /// <param name="sourceWhitePoint">White point of the source color</param>
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

            var fx3 = MathUtils.Pow3(fx);
            var fz3 = MathUtils.Pow3(fz);

            var xr = fx3 > CIEConstants.Epsilon ? fx3 : (116 * fx - 16) / CIEConstants.Kappa;
            var yr = L > CIEConstants.Kappa * CIEConstants.Epsilon ? MathUtils.Pow3((L + 16) / 116d) : L / CIEConstants.Kappa;
            var zr = fz3 > CIEConstants.Epsilon ? fz3 : (116 * fz - 16) / CIEConstants.Kappa;

            double Xr = _sourceWhitePoint.X, Yr = _sourceWhitePoint.Y, Zr = _sourceWhitePoint.Z;

            // avoids XYZ coordinates out range (restricted by 0 and XYZ reference white)
            xr = xr.CropRange(min: 0, max: 1);
            yr = yr.CropRange(min: 0, max: 1);
            zr = zr.CropRange(min: 0, max: 1);

            var X = xr * Xr;
            var Y = yr * Yr;
            var Z = zr * Zr;

            var targetColor = new XYZColor(in X, in Y, in Z);
            return targetColor;
        }
    }
}