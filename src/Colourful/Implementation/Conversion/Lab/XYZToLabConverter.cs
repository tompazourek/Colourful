using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="XYZColor" /> to <see cref="LabColor" />.
    /// </summary>
    public sealed class XYZToLabConverter : IColorConversion<XYZColor, LabColor>
    {
        /// <summary>
        /// Constructs with <see cref="LabColor.DefaultWhitePoint" />
        /// </summary>
        public XYZToLabConverter()
            : this(LabColor.DefaultWhitePoint)
        {
        }

        /// <summary>
        /// Constructs with arbitrary white point
        /// </summary>
        public XYZToLabConverter(XYZColor labWhitePoint)
        {
            LabWhitePoint = labWhitePoint;
        }

        /// <summary>
        /// Target reference white. When not set, <see cref="LabColor.DefaultWhitePoint" /> is used.
        /// </summary>
        public XYZColor LabWhitePoint { get; }

        /// <summary>
        /// Converts from <see cref="XYZColor" /> to <see cref="LabColor" />.
        /// </summary>
        public LabColor Convert(in XYZColor input)
        {
            // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Lab.html
            double Xr = LabWhitePoint.X, Yr = LabWhitePoint.Y, Zr = LabWhitePoint.Z;

            double xr = input.X / Xr, yr = input.Y / Yr, zr = input.Z / Zr;

            var fx = f(xr);
            var fy = f(yr);
            var fz = f(zr);

            var L = 116 * fy - 16;
            var a = 500 * (fx - fy);
            var b = 200 * (fy - fz);

            var output = new LabColor(L, a, b, LabWhitePoint);
            return output;
        }

        private static double f(double cr)
        {
            var fc = cr > CIEConstants.Epsilon ? Math.Pow(cr, 1 / 3d) : (CIEConstants.Kappa * cr + 16) / 116d;
            return fc;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public bool Equals(XYZToLabConverter other)
        {
            if (other == null)
                return false;

            return ReferenceEquals(this, other) || LabWhitePoint.Equals(other.LabWhitePoint);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is XYZToLabConverter other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => LabWhitePoint.GetHashCode();

        /// <inheritdoc cref="object" />
        public static bool operator ==(XYZToLabConverter left, XYZToLabConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(XYZToLabConverter left, XYZToLabConverter right) => !Equals(left, right);

        #endregion
    }
}