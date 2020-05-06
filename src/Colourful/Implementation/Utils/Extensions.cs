using System;



namespace Colourful.Implementation
{
    internal static class Extensions
    {
        public static double CheckRange(this double value, double min, double max)
        {
            if (value < min)
                throw new ArgumentOutOfRangeException(nameof(value), value, "The minimum value is " + min);

            if (value > max)
                throw new ArgumentOutOfRangeException(nameof(value), value, "The maximum value is " + max);

            return value;
        }

        public static double CropRange(this double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public static double[] CropRange(this double[] vector, double min, double max)
        {
            var croppedVector = new double[vector.Length];

            for (var i = 0; i < vector.Length; i++)
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

            // ReSharper disable once CoVariantArrayConversion
            return croppedVector;
        }

        /// <summary>
        /// Matrix inverse for 3 by 3 matrices
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double[][] Inverse(this double[][] matrix)
        {
            if (matrix.Length != 3 || matrix[0].Length != 3)
                throw new ArgumentOutOfRangeException(nameof(matrix), "Inversion is supported only on 3 by 3 matrices.");

            var A = matrix[1][1] * matrix[2][2] - matrix[1][2] * matrix[2][1];
            var D = -(matrix[0][1] * matrix[2][2] - matrix[0][2] * matrix[2][1]);
            var G = matrix[0][1] * matrix[1][2] - matrix[0][2] * matrix[1][1];
            var B = -(matrix[1][0] * matrix[2][2] - matrix[1][2] * matrix[2][0]);
            var E = matrix[0][0] * matrix[2][2] - matrix[0][2] * matrix[2][0];
            var H = -(matrix[0][0] * matrix[1][2] - matrix[0][2] * matrix[1][0]);
            var C = matrix[1][0] * matrix[2][1] - matrix[1][1] * matrix[2][0];
            var F = -(matrix[0][0] * matrix[2][1] - matrix[0][1] * matrix[2][0]);
            var I = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
            var det = matrix[0][0] * A + matrix[0][1] * B + matrix[0][2] * C;
            double[][] result = new[]
            {
                new[] { A / det, D / det, G / det },
                new[] { B / det, E / det, H / det },
                new[] { C / det, F / det, I / det },
            };
            return result;
        }

        public static double[] MultiplyBy(this double[][] matrix, double[] vector)
        {
            if (matrix[0].Length != vector.Length)
                throw new ArgumentOutOfRangeException(nameof(matrix), "Non-conformable matrices and vectors cannot be multiplied.");

            var result = new double[matrix.Length];

            for (var i = 0; i < matrix.Length; ++i) // each row of matrix
            {
                for (var k = 0; k < vector.Length; ++k) // each element of vector
                {
                    result[i] += matrix[i][k] * vector[k];
                }
            }

            // ReSharper disable once CoVariantArrayConversion
            return result;
        }

        public static double[][] MultiplyBy(this double[][] matrix1, double[][] matrix2)
        {
            if (matrix1[0].Length != matrix2.Length)
                throw new ArgumentOutOfRangeException(nameof(matrix1), "Non-conformable matrices cannot be multiplied.");

            var result = MatrixFactory.CreateEmpty(matrix1.Length, matrix2[0].Length);

            for (var i = 0; i < matrix1.Length; ++i) // each row of 1
            {
                for (var j = 0; j < matrix2[0].Length; ++j) // each column of 2
                {
                    for (var k = 0; k < matrix1[0].Length; ++k)
                    {
                        result[i][j] += matrix1[i][k] * matrix2[k][j];
                    }
                }
            }

            // ReSharper disable once CoVariantArrayConversion
            return result;
        }
    }
}