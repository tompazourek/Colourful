#region License

// Copyright (C) Tomáš Pažourek, 2016
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
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