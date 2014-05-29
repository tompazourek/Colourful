#region License

// Copyright (C) Tomáš Pažourek, 2014
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
using System.Threading.Tasks;
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.Implementation
{
    internal static class MatrixFactory
    {
        public static double[][] CreateEmpty(int rows, int columns)
        {
            var result = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[columns];
                for (int j = 0; j < columns; j++)
                    result[i][j] = 0;
            }
            return result;
        }

        public static Matrix CreateIdentity(int size)
        {
            var result = new double[size][];
            for (int i = 0; i < size; i++)
            {
                result[i] = new double[size];
                for (int j = 0; j < size; j++)
                    result[i][j] = i == j ? 1 : 0;
            }
            return result;
        }

        public static Matrix CreateDiagonal(params double[] items)
        {
            var size = items.Length;
            var result = new double[size][];
            for (int i = 0; i < size; i++)
            {
                result[i] = new double[size];
                for (int j = 0; j < size; j++)
                    result[i][j] = i == j ? items[i] : 0;
            }
            return result;
        }
    }
}