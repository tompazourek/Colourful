namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor" /> to <see cref="XYZColor" />.
    /// </summary>
    public sealed class LabToXYZConverter : IColorConversion<LabColor, XYZColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LabToXYZConverter Default = new LabToXYZConverter();

        /// <summary>
        /// Converts from <see cref="LabColor" /> to <see cref="XYZColor" />.
        /// </summary>
        public XYZColor Convert(in LabColor input)
        {
            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_Lab_to_XYZ.html
            double L = input.L, a = input.a, b = input.b;
            var fy = (L + 16) / 116d;
            var fx = a / 500d + fy;
            var fz = fy - b / 200d;

            var fx3 = MathUtils.Pow3(fx);
            var fz3 = MathUtils.Pow3(fz);

            var xr = fx3 > CIEConstants.Epsilon ? fx3 : (116 * fx - 16) / CIEConstants.Kappa;
            var yr = L > CIEConstants.Kappa * CIEConstants.Epsilon ? MathUtils.Pow3((L + 16) / 116d) : L / CIEConstants.Kappa;
            var zr = fz3 > CIEConstants.Epsilon ? fz3 : (116 * fz - 16) / CIEConstants.Kappa;

            double Xr = input.WhitePoint.X, Yr = input.WhitePoint.Y, Zr = input.WhitePoint.Z;

            // avoids XYZ coordinates out range (restricted by 0 and XYZ reference white)
            xr = xr.CropRange(0, 1);
            yr = yr.CropRange(0, 1);
            zr = zr.CropRange(0, 1);

            var X = xr * Xr;
            var Y = yr * Yr;
            var Z = zr * Zr;

            var result = new XYZColor(X, Y, Z);
            return result;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LabToXYZConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LabToXYZConverter left, LabToXYZConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LabToXYZConverter left, LabToXYZConverter right) => !Equals(left, right);

        #endregion
    }
}