using static System.Math;

namespace Colourful.Internals;

/// <inheritdoc />
public class JzazbzToXYZConverter : IColorConverter<JzazbzColor, XYZColor>
{
    /// <inheritdoc />
    public XYZColor Convert(in JzazbzColor sourceColor)
    {
        // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

        var Jz = sourceColor.Jz;
        var az = sourceColor.az;
        var bz = sourceColor.bz;

        Jz = Jz + 1.6295499532821566e-11;
        var Iz = Jz / (0.44 + 0.56 * Jz);
        var L = PerceptualQuantizerInverse(Iz + 1.386050432715393e-1 * az + 5.804731615611869e-2 * bz);
        var M = PerceptualQuantizerInverse(Iz - 1.386050432715393e-1 * az - 5.804731615611891e-2 * bz);
        var S = PerceptualQuantizerInverse(Iz - 9.601924202631895e-2 * az - 8.118918960560390e-1 * bz);

        var X = +1.661373055774069e+00 * L - 9.145230923250668e-01 * M + 2.313620767186147e-01 * S;
        var Y = -3.250758740427037e-01 * L + 1.571847038366936e+00 * M - 2.182538318672940e-01 * S;
        var Z = -9.098281098284756e-02 * L - 3.127282905230740e-01 * M + 1.522766561305260e+00 * S;

        var targetColor = new XYZColor(X / 10000d, Y / 10000d, Z / 10000d);
        return targetColor;
    }

    private static double PerceptualQuantizerInverse(double X)
    {
        var XX = Pow(X, y: 7.460772656268214e-03);
        var result = 1e4 * Pow((0.8359375 - XX) / (18.6875 * XX - 18.8515625), y: 6.277394636015326);
        return result;
    }
}
