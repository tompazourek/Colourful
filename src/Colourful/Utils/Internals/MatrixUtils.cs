namespace Colourful.Internals
{
    internal static class MatrixUtils
    {
        public static double[,] CreateIdentity(in int size)
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

        /// <summary>
        /// Matrix inverse for 3 by 3 matrices.
        /// </summary>
        public static double[,] Inverse(in double[,] matrix)
        {
            var A = matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1];
            var D = -(matrix[0, 1] * matrix[2, 2] - matrix[0, 2] * matrix[2, 1]);
            var G = matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1];
            var B = -(matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]);
            var E = matrix[0, 0] * matrix[2, 2] - matrix[0, 2] * matrix[2, 0];
            var H = -(matrix[0, 0] * matrix[1, 2] - matrix[0, 2] * matrix[1, 0]);
            var C = matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0];
            var F = -(matrix[0, 0] * matrix[2, 1] - matrix[0, 1] * matrix[2, 0]);
            var I = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            var det = matrix[0, 0] * A + matrix[0, 1] * B + matrix[0, 2] * C;
            double[,] result =
            {
                { A / det, D / det, G / det },
                { B / det, E / det, H / det },
                { C / det, F / det, I / det },
            };
            return result;
        }

        public static double[] MultiplyBy(in double[,] matrix, in double[] vector)
        {
            var vectorLength = vector.Length;
            var matrixLength0 = matrix.GetLength(dimension: 0);
            var result = new double[matrixLength0];

            for (var i = 0; i < matrixLength0; ++i) // each row of matrix
            {
                for (var k = 0; k < vectorLength; ++k) // each element of vector
                {
                    result[i] += matrix[i, k] * vector[k];
                }
            }

            return result;
        }
    }
}
