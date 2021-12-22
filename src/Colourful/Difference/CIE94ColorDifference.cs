using static System.Math;
using static Colourful.CIE94ColorDifferenceApplication;
using static Colourful.Internals.MathUtils;

namespace Colourful;

/// <summary>
/// CIE Delta-E 1994 color difference formula.
/// </summary>
/// <remarks>
/// Implementation notes:
/// http://www.brucelindbloom.com/Eqn_DeltaE_CIE94.html
/// </remarks>
public class CIE94ColorDifference : IColorDifference<LabColor>
{
    private const double KH = 1;
    private const double KC = 1;

    private readonly double K1;
    private readonly double K2;
    private readonly double KL;

    /// <summary>
    /// Construct using weighting factors for <see cref="CIE94ColorDifferenceApplication.GraphicArts" />.
    /// </summary>
    public CIE94ColorDifference() : this(GraphicArts)
    {
    }

    /// <summary>
    /// Construct using weighting factors for given application of color difference.
    /// </summary>
    /// <param name="application">A <see cref="CIE94ColorDifferenceApplication" /> value specifying the application area. Different weighting factors are used in the computation depending on the application.</param>
    public CIE94ColorDifference(in CIE94ColorDifferenceApplication application)
    {
        if (application == Textiles)
        {
            KL = 2;
            K1 = 0.048;
            K2 = 0.014;
        }
        else
        {
            // GraphicArts
            KL = 1;
            K1 = 0.045;
            K2 = 0.015;
        }
    }

    /// <param name="x">Reference color.</param>
    /// <param name="y">Sample color.</param>
    /// <returns>Delta-E (1994) color difference.</returns>
    public double ComputeDifference(in LabColor x, in LabColor y)
    {
        var da = x.a - y.a;
        var db = x.b - y.b;
        var dL = x.L - y.L;
        var C1 = Sqrt(x.a * x.a + x.b * x.b);
        var C2 = Sqrt(y.a * y.a + y.b * y.b);
        var dC = C1 - C2;
        var dH_sq = da * da + db * db - dC * dC; // dH ^ 2
        const double SL = 1;
        var SC = 1 + K1 * C1;
        var SH = 1 + K2 * C1;
        var dE94 = Sqrt
        (
            Pow2(dL / (KL * SL)) +
            Pow2(dC / (KC * SC)) +
            dH_sq / Pow2(KH * SH)
        );
        return dE94;
    }
}
