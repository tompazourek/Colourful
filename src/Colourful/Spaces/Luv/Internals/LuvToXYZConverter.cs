using static System.Double;
using static Colourful.Internals.CIEConstants;
using static Colourful.Internals.MathUtils;

namespace Colourful.Internals;

/// <inheritdoc />
public class LuvToXYZConverter : IColorConverter<LuvColor, XYZColor>
{
    private readonly XYZColor _sourceWhitePoint;

    /// <param name="sourceWhitePoint">White point of the source color.</param>
    public LuvToXYZConverter(in XYZColor sourceWhitePoint) => _sourceWhitePoint = sourceWhitePoint;

    /// <inheritdoc />
    public XYZColor Convert(in LuvColor sourceColor)
    {
        // conversion algorithm described here: http://www.brucelindbloom.com/index.html?Eqn_Luv_to_XYZ.html
        double L = sourceColor.L, u = sourceColor.u, v = sourceColor.v;

        var u0 = Compute_u0(_sourceWhitePoint);
        var v0 = Compute_v0(_sourceWhitePoint);

        var Y = L > Kappa * CIEConstants.Epsilon
            ? Pow3((L + 16) / 116)
            : L / Kappa;

        var a = (52 * L / (u + 13 * L * u0) - 1) / 3;
        var b = -5 * Y;
        var c = -1 / 3d;
        var d = Y * (39 * L / (v + 13 * L * v0) - 5);

        var X = (d - b) / (a - c);
        var Z = X * a + b;

        if (IsNaN(X) || X < 0)
            X = 0;

        if (IsNaN(Y) || Y < 0)
            Y = 0;

        if (IsNaN(Z) || Z < 0)
            Z = 0;

        var targetColor = new XYZColor(in X, in Y, in Z);
        return targetColor;
    }

    private static double Compute_u0(in XYZColor input) => 4 * input.X / (input.X + 15 * input.Y + 3 * input.Z);

    private static double Compute_v0(in XYZColor input) => 9 * input.Y / (input.X + 15 * input.Y + 3 * input.Z);
}
