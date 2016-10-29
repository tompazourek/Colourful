﻿#region License

// Copyright (C) Tomáš Pažourek, 2016
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

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

        public static void AssignVariables(this Vector vector, out double component1, out double component2, out double component3)
        {
            if (vector.Count != 3)
                throw new ArgumentOutOfRangeException(nameof(vector), "Vector must have 3 components.");

            component1 = vector[0];
            component2 = vector[1];
            component3 = vector[2];
        }

        public static double EuclideanDistance(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
                throw new ArgumentException("Vectors are of different size.");

            var sum = vector1.Select((t, i) => (t - vector2[i])*(t - vector2[i])).Sum();
            var root = Math.Sqrt(sum);
            return root;
        }

        /// <summary>
        /// Matrix inverse for 3 by 3 matrices
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Matrix Inverse(this Matrix matrix)
        {
            if (matrix.Count != 3 || matrix[0].Count != 3)
                throw new ArgumentOutOfRangeException(nameof(matrix), "Inversion is supported only on 3 by 3 matrices.");

            var A = (matrix[1][1]*matrix[2][2] - matrix[1][2]*matrix[2][1]);
            var D = -(matrix[0][1]*matrix[2][2] - matrix[0][2]*matrix[2][1]);
            var G = (matrix[0][1]*matrix[1][2] - matrix[0][2]*matrix[1][1]);
            var B = -(matrix[1][0]*matrix[2][2] - matrix[1][2]*matrix[2][0]);
            var E = (matrix[0][0]*matrix[2][2] - matrix[0][2]*matrix[2][0]);
            var H = -(matrix[0][0]*matrix[1][2] - matrix[0][2]*matrix[1][0]);
            var C = (matrix[1][0]*matrix[2][1] - matrix[1][1]*matrix[2][0]);
            var F = -(matrix[0][0]*matrix[2][1] - matrix[0][1]*matrix[2][0]);
            var I = (matrix[0][0]*matrix[1][1] - matrix[0][1]*matrix[1][0]);
            var det = matrix[0][0]*A + matrix[0][1]*B + matrix[0][2]*C;
            Matrix result = new Vector[]
            {
                new[] { A/det, D/det, G/det },
                new[] { B/det, E/det, H/det },
                new[] { C/det, F/det, I/det },
            };
            return result;
        }

        public static Vector MultiplyBy(this Matrix matrix, Vector vector)
        {
            Matrix vectorAsMatrix = vector.Select(x => new[] { x }).Cast<Vector>().ToArray();
            Matrix resultAsMatrix = matrix.MultiplyBy(vectorAsMatrix);
            Vector result = resultAsMatrix.SelectMany(x => x).ToArray();
            return result;
        }

        public static Matrix MultiplyBy(this Matrix matrix1, Matrix matrix2)
        {
            if (matrix1[0].Count != matrix2.Count)
                throw new ArgumentOutOfRangeException(nameof(matrix1), "Non-conformable matrices cannot be multiplied.");

            var result = MatrixFactory.CreateEmpty(matrix1.Count, matrix2[0].Count);

            for (var i = 0; i < matrix1.Count; ++i) // each row of 1
                for (var j = 0; j < matrix2[0].Count; ++j) // each column of 2
                    for (var k = 0; k < matrix1[0].Count; ++k)
                        result[i][j] += matrix1[i][k]*matrix2[k][j];

            // ReSharper disable once CoVariantArrayConversion
            return result;
        }
    }
}