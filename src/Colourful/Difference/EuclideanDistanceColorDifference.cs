using System;

namespace Colourful.Difference
{
    /// <summary>
    /// Euclidean distance between two color vectors.
    /// </summary>
    public sealed class EuclideanDistanceColorDifference : IColorDifference<IColorVector>
    {
        /// <summary>
        /// Computes distance between two color vectors as euclidean distance.
        /// </summary>
        public double ComputeDifference(in IColorVector x, in IColorVector y)
        {
            var distanceSquared = 0d;
            var vectorSize = Math.Min(x.Vector.Count, y.Vector.Count);

            for (var i = 0; i < vectorSize; i++)
            {
                var xi = x.Vector[i];
                var yi = y.Vector[i];

                var xyiDiff = xi - yi;
                distanceSquared += xyiDiff * xyiDiff;
            }

            var distance = Math.Sqrt(distanceSquared);
            return distance;
        }
    }
}