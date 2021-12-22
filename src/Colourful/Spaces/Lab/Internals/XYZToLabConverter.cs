using static System.Math;
using static Colourful.Internals.CIEConstants;

namespace Colourful.Internals;

/// <inheritdoc />
public class XYZToLabConverter : IColorConverter<XYZColor, LabColor>
{
    private readonly XYZColor _targetWhitePoint;

    /// <param name="targetWhitePoint">White point of the target color.</param>
    public XYZToLabConverter(in XYZColor targetWhitePoint) => _targetWhitePoint = targetWhitePoint;

    /// <inheritdoc />
    public LabColor Convert(in XYZColor sourceColor)
    {
        // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Lab.html
        double Xr = _targetWhitePoint.X, Yr = _targetWhitePoint.Y, Zr = _targetWhitePoint.Z;

        double xr = sourceColor.X / Xr, yr = sourceColor.Y / Yr, zr = sourceColor.Z / Zr;

        var fx = f(xr);
        var fy = f(yr);
        var fz = f(zr);

        var L = 116 * fy - 16;
        var a = 500 * (fx - fy);
        var b = 200 * (fy - fz);

        var targetColor = new LabColor(in L, in a, in b);
        return targetColor;
    }

    private static double f(double cr)
    {
        var fc = cr > Epsilon ? Pow(cr, 1 / 3d) : (Kappa * cr + 16) / 116d;
        return fc;
    }
}
