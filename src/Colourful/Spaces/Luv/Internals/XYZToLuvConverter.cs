using static System.Math;
using static Colourful.Internals.CIEConstants;

namespace Colourful.Internals;

/// <inheritdoc />
public class XYZToLuvConverter : IColorConverter<XYZColor, LuvColor>
{
    private readonly XYZColor _targetWhitePoint;

    /// <param name="targetWhitePoint">White point of the target color.</param>
    public XYZToLuvConverter(in XYZColor targetWhitePoint) => _targetWhitePoint = targetWhitePoint;

    /// <inheritdoc />
    public LuvColor Convert(in XYZColor sourceColor)
    {
        // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Luv.html

        var yr = sourceColor.Y / _targetWhitePoint.Y;
        var up = Compute_up(sourceColor);
        var vp = Compute_vp(sourceColor);
        var upr = Compute_up(_targetWhitePoint);
        var vpr = Compute_vp(_targetWhitePoint);

        var L = yr > CIEConstants.Epsilon ? 116 * Pow(yr, 1 / 3d) - 16 : Kappa * yr;

        if (double.IsNaN(L) || L < 0)
            L = 0;

        var u = 13 * L * (up - upr);
        var v = 13 * L * (vp - vpr);

        if (double.IsNaN(u))
            u = 0;

        if (double.IsNaN(v))
            v = 0;

        var targetColor = new LuvColor(in L, in u, in v);
        return targetColor;
    }

    private static double Compute_up(XYZColor input) => 4 * input.X / (input.X + 15 * input.Y + 3 * input.Z);

    private static double Compute_vp(XYZColor input) => 9 * input.Y / (input.X + 15 * input.Y + 3 * input.Z);
}
