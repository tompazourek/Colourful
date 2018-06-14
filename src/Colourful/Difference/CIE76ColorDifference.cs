using System;

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 1976 formula
    /// </summary>
    public sealed class CIE76ColorDifference : IColorDifference<LabColor>
    {
        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <returns>Delta-E (1976) color difference</returns>
        public double ComputeDifference(in LabColor x, in LabColor y)
        {
            if (x.WhitePoint != y.WhitePoint)
                throw new ArgumentException("Colors must have same white point to be compared.");

            // Euclidean distance
            var distance = Math.Sqrt(
                (x.L - y.L) * (x.L - y.L) +
                (x.a - y.a) * (x.a - y.a) +
                (x.b - y.b) * (x.b - y.b)
            );
            return distance;
        }
    }
}