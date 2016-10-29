﻿#region License

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

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two doubles and takes only specific number of fractional digits into account.
    /// </summary>
    public class DoublePrecisionComparer : IComparer<double>
    {
        /// <param name="precision"><see cref="Precision"/></param>
        public DoublePrecisionComparer(int precision)
        {
            Precision = precision;
        }

        /// <summary>
        /// Number of fractional digits
        /// </summary>
        public int Precision { get; }

        public int Compare(double x, double y)
        {
            double xp = FloorWithPrecision(x, Precision);
            double yp = FloorWithPrecision(y, Precision);

            int result = Comparer<double>.Default.Compare(xp, yp);
            return result;
        }

        /// <summary>
        /// Floors number and preserves specific numer of decimal places.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        private static double FloorWithPrecision(double input, int decimalPlaces)
        {
            double power = Math.Pow(10, decimalPlaces);
            double output = Math.Floor(input * power) / power;
            return output;
        }
    }
}