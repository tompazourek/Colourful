using static System.Math;

namespace Colourful
{
    /// <summary>
    /// Euclidean distance between two color vectors.
    /// </summary>
    public sealed class EuclideanDistanceColorDifference<TColor> : IColorDifference<TColor>
        where TColor : IColorSpace, IColorVector
    {
        /// <summary>
        /// Computes distance between two color vectors as euclidean distance.
        /// </summary>
        public double ComputeDifference(in TColor x, in TColor y)
        {
            var distanceSquared = 0d;
            var vectorSize = Min(x.Vector.Length, y.Vector.Length);

            for (var i = 0; i < vectorSize; i++)
            {
                var xi = x.Vector[i];
                var yi = y.Vector[i];

                var xyiDiff = xi - yi;
                distanceSquared += xyiDiff * xyiDiff;
            }

            var distance = Sqrt(distanceSquared);
            return distance;
        }
    }
}