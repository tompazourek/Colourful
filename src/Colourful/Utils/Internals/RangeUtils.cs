namespace Colourful.Internals
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
    }
}