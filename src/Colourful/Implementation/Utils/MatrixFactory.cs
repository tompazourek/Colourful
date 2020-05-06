namespace Colourful.Implementation
{
    internal static class MatrixFactory
    {
        public static double[,] CreateIdentity(int size)
        {
            var result = new double[size, size];

            for (var i = 0; i < size; i++)
            {
                result[i, i] = 1;
            }

            return result;
        }

        public static double[,] CreateDiagonal(params double[] items)
        {
            var size = items.Length;
            var result = new double[size, size];

            for (var i = 0; i < size; i++)
            {
                result[i, i] = items[i];
            }

            return result;
        }
    }
}