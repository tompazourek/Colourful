using static System.Math;
using static Colourful.Internals.MathUtils;

namespace Colourful;

/// <summary>
/// Formulas useful for cylindrical color spaces (<see cref="LChabColor" /> and <see cref="LChuvColor" />).
/// </summary>
public static class CylindricalFormulas
{
    /// <summary>
    /// Returns saturation of the color (chroma normalized by lightness).
    /// </summary>
    public static double GetSaturation(in double L, in double C)
    {
        var result = 100 * (C / L);

        if (double.IsNaN(result))
            return 0;

        return result;
    }

    /// <summary>
    /// Gets chroma from saturation and lightness.
    /// </summary>
    public static double GetChroma(in double saturation, in double L)
    {
        var result = L * (saturation / 100);
        return result;
    }

    /// <summary>
    /// Converts a vector in form of Lightness, Chroma1, Chroma2 into Lightness, Chroma, Hue.
    /// </summary>
    public static double[] ConvertToLCh(in double[] sourceVector)
    {
        double chroma1 = sourceVector[1], chroma2 = sourceVector[2];
        var C = Sqrt(chroma1 * chroma1 + chroma2 * chroma2);
        var hRadians = Atan2(chroma2, chroma1);
        var hDegrees = NormalizeDegree(RadianToDegree(in hRadians));
        var targetVector = new[] { sourceVector[0], C, hDegrees };
        return targetVector;
    }

    /// <summary>
    /// Converts a vector in form of Lightness, Chroma, Hue into Lightness, Chroma1, Chroma2.
    /// </summary>
    public static double[] ConvertFromLCh(in double[] sourceVector)
    {
        double C = sourceVector[1], hDegrees = sourceVector[2];
        var hRadians = DegreeToRadian(in hDegrees);

        var a = C * Cos(hRadians);
        var b = C * Sin(hRadians);

        var targetVector = new[] { sourceVector[0], a, b };
        return targetVector;
    }
}
