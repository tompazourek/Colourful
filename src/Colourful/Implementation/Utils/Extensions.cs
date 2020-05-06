using System;

namespace Colourful.Implementation
{
    internal static class Extensions
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

        /// <summary>
        /// Matrix inverse for 3 by 3 matrices
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double[,] Inverse(this double[,] matrix)
        {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
                throw new ArgumentOutOfRangeException(nameof(matrix), "Inversion is supported only on 3 by 3 matrices.");

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

        public static double[] MultiplyBy(this double[,] matrix, double[] vector)
        {
            var vectorLength = vector.Length;
            if (matrix.GetLength(1) != vectorLength)
                throw new ArgumentOutOfRangeException(nameof(matrix), "Non-conformable matrices and vectors cannot be multiplied.");

            var matrixLength0 = matrix.GetLength(0);
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

        public static double[,] MultiplyBy(this double[,] matrix1, double[,] matrix2)
        {
            var matrix1Length1 = matrix1.GetLength(1);
            var matrix2Length0 = matrix2.GetLength(0);

            if (matrix1Length1 != matrix2Length0)
                throw new ArgumentOutOfRangeException(nameof(matrix1), "Non-conformable matrices cannot be multiplied.");

            var matrix1Length0 = matrix1.GetLength(0);
            var matrix2Length1 = matrix2.GetLength(1);

            var result = new double[matrix1Length0, matrix2Length1];

            for (var i = 0; i < matrix1Length0; ++i) // each row of 1
            {
                for (var j = 0; j < matrix2Length1; ++j) // each column of 2
                {
                    for (var k = 0; k < matrix1Length1; ++k)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return result;
        }
    }
}