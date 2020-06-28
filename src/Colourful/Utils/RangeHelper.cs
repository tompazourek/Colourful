using System.Linq;

namespace Colourful
{
    /// <summary>
    /// Utilities to crop range of values or vectors.
    /// </summary>
    public static class RangeHelper
    {
        /// <summary>
        /// If the given value is less than the minimum, returns the minimum value.
        /// If the given value is greater than the maximum, returns the maximum value.
        /// Otherwise, returns the value
        /// </summary>
        public static double CropRange(this double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        /// <summary>
        /// If any value is less than the minimum, replace it with the minimum value.
        /// If any value is greater than the maximum, replace it with the maximum value.
        /// Otherwise, copy the value.
        /// </summary>
        public static double[] CropRange(this double[] vector, double min, double max)
        {
            var croppedVector = vector.Select(x => x.CropRange(min, max)).ToArray();
            return croppedVector;
        }

        /// <summary>
        /// If any value is less than the corresponding minimum, replace it with the minimum value.
        /// If any value is greater than the corresponding maximum, replace it with the maximum value.
        /// Otherwise, copy the value.
        /// </summary>
        public static double[] CropRange(this double[] vector, double[] mins, double[] maxs)
        {
            var croppedVector = vector.Select((x, index) => x.CropRange(mins[index], maxs[index])).ToArray();
            return croppedVector;
        }
    }
}