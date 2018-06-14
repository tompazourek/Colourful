using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation
{
    internal static class MatrixFactory
    {
        public static double[][] CreateEmpty(int rows, int columns)
        {
            var result = new double[rows][];
            for (var i = 0; i < rows; i++)
            {
                result[i] = new double[columns];
            }

            return result;
        }

        public static Matrix CreateIdentity(int size)
        {
            var result = new double[size][];
            for (var i = 0; i < size; i++)
            {
                result[i] = new double[size];
                result[i][i] = 1;
            }

            // ReSharper disable once CoVariantArrayConversion
            return result;
        }

        public static Matrix CreateDiagonal(params double[] items)
        {
            var size = items.Length;
            var result = new double[size][];
            for (var i = 0; i < size; i++)
            {
                result[i] = new double[size];
                result[i][i] = items[i];
            }

            // ReSharper disable once CoVariantArrayConversion
            return result;
        }
    }
}