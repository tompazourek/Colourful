#region License

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

namespace Colourful.Implementation
{
    /// <summary>
    /// Math helper functions
    /// </summary>
    internal static class MathUtils
    {
        /// <summary>
        /// Compute x^2
        /// </summary>
        /// <param name="x">Base</param>
        /// <returns>Result of the exponentiation</returns>
        public static double Pow2(double x)
        {
            return x*x;
        }

        /// <summary>
        /// Compute x^3
        /// </summary>
        /// <param name="x">Base</param>
        /// <returns>Result of the exponentiation</returns>
        public static double Pow3(double x)
        {
            return x*x*x;
        }

        /// <summary>
        /// Compute x^4
        /// </summary>
        /// <param name="x">Base</param>
        /// <returns>Result of the exponentiation</returns>
        public static double Pow4(double x)
        {
            return (x*x)*(x*x);
        }

        /// <summary>
        /// Compute x^7
        /// </summary>
        /// <param name="x">Base</param>
        /// <returns>Result of the exponentiation</returns>
        public static double Pow7(double x)
        {
            return (x*x*x)*(x*x*x)*x;
        }

        /// <summary>
        /// Compute sine of angle in degrees
        /// </summary>
        /// <param name="x">Given angle</param>
        /// <returns></returns>
        public static double SinDeg(double x)
        {
            var x_rad = Angle.DegreeToRadian(x);
            var y = Math.Sin(x_rad);
            return y;
        }

        /// <summary>
        /// Compute cosine of angle in degrees
        /// </summary>
        /// <param name="x">Given angle</param>
        /// <returns></returns>
        public static double CosDeg(double x)
        {
            var x_rad = Angle.DegreeToRadian(x);
            var y = Math.Cos(x_rad);
            return y;
        }
    }
}