namespace Colourful
{
    internal static class RangeUtils
    {
        public static double CropRange(in this double value, in double min, in double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static double[] CropRange(this double[] vector, in double min, in double max)
        {
            var vectorLength = vector.Length;
            var croppedVector = new double[vectorLength];

            for (var i = 0; i < vectorLength; i++)
            {
                if (vector[i] < min)
                {
                    croppedVector[i] = min;
                }
                else if (vector[i] > max)
                {
                    croppedVector[i] = max;
                }
                else
                {
                    croppedVector[i] = vector[i];
                }
            }

            return croppedVector;
        }
    }
}