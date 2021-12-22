using static System.Math;

namespace Colourful;

/// <summary>
/// CIE Delta-E 1976 color difference formula.
/// </summary>
public class CIE76ColorDifference : IColorDifference<LabColor>
{
    /// <param name="x">Reference color.</param>
    /// <param name="y">Sample color.</param>
    /// <returns>Delta-E (1976) color difference.</returns>
    public double ComputeDifference(in LabColor x, in LabColor y)
    {
        // Euclidean distance
        var distance = Sqrt(
            (x.L - y.L) * (x.L - y.L) +
            (x.a - y.a) * (x.a - y.a) +
            (x.b - y.b) * (x.b - y.b)
        );
        return distance;
    }
}
