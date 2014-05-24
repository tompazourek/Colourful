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
using MathNet.Numerics;

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 2000 formula
    /// </summary>
    public class CIEDE2000ColorDifference : IColorDifference<LabColor>
    {
        // parametric weighing factors:
        private const double k_H = 1;
        private const double k_L = 1;
        private const double k_C = 1;

        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <remarks>Implemented according to: Sharma, Gaurav; Wencheng Wu, Edul N. Dalal (2005). "The CIEDE2000 color-difference formula: Implementation notes, supplementary test data, and mathematical observations" (http://www.ece.rochester.edu/~gsharma/ciede2000/ciede2000noteCRNA.pdf)</remarks>
        /// <returns>Delta-E (2000) color difference</returns>
        public double ComputeDifference(LabColor x, LabColor y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            if (x.WhitePoint != y.WhitePoint)
                throw new ArgumentException("Colors must have same white point to be compared.");

            var L = new[] { x.L, y.L };
            var a = new[] { x.a, y.a };
            var b = new[] { x.b, y.b };

            // 1. Calculate C_prime, h_prime
            double[] a_prime = Calculate_a_prime(a, b);
            double[] C_prime = Calculate_C_prime(a_prime, b);
            double[] h_prime = Calculate_h_prime(a_prime, b);

            // 2. Calculate dL_prime, dC_prime, dH_prime
            double dL_prime = L[1] - L[0]; // eq. (8)
            double dC_prime = C_prime[1] - C_prime[0]; // eq. (9)
            double dh_prime = Calculate_dh_prime(C_prime, h_prime);
            double dH_prime = 2 * Math.Sqrt(C_prime[0] * C_prime[1]) * SinDeg(dh_prime / 2); // eq. (11)

            // 3. Calculate CIEDE2000 Color-Difference dE00
            double L_prime_mean = (L[0] + L[1]) / 2; // eq. (12)
            double C_prime_mean = (C_prime[0] + C_prime[1]) / 2; // eq. (13)
            double h_prime_mean = Calculate_h_prime_mean(h_prime, C_prime);
            double T = 1 - 0.17 * CosDeg(h_prime_mean - 30) + 0.24 * CosDeg(2 * h_prime_mean)
                + 0.32 * CosDeg(3 * h_prime_mean + 6) - 0.20 * CosDeg(4 * h_prime_mean - 63); // eq. (15)
            double dTheta = 30 * Math.Exp(-Math.Pow(((h_prime_mean - 275) / 25), 2)); // eq. (16)
            double R_C = 2 * Math.Sqrt(Math.Pow(C_prime_mean, 7) / (Math.Pow(C_prime_mean, 7) + Math.Pow(25, 7))); // eq. (17)
            double S_L = 1 + (0.015 * Math.Pow(L_prime_mean - 50, 2)) / Math.Sqrt(20 + Math.Pow(L_prime_mean - 50, 2)); // eq. (18)
            double S_C = 1 + 0.045 * C_prime_mean; // eq. (19)
            double S_H = 1 + 0.015 * C_prime_mean * T; // eq. (20)
            double R_T = -SinDeg(2 * dTheta) * R_C; // eq. (21)

            double dE00 = Math.Sqrt(
                Math.Pow(dL_prime / (k_L * S_L), 2) +
                    Math.Pow(dC_prime / (k_C * S_C), 2) +
                    Math.Pow(dH_prime / (k_H * S_H), 2) +
                    R_T * (dC_prime / (k_C * S_C)) * (dH_prime / (k_H * S_H))
                ); // eq. (22)

            return dE00;
        }

        /// <summary>
        /// Compute sine of angle in degrees
        /// </summary>
        /// <param name="x">Given angle</param>
        /// <returns></returns>
        private static double SinDeg(double x)
        {
            double x_rad = Trig.DegreeToRadian(x);
            double y = Math.Sin(x_rad);
            return y;
        }

        /// <summary>
        /// Compute cosine of angle in degrees
        /// </summary>
        /// <param name="x">Given angle</param>
        /// <returns></returns>
        private static double CosDeg(double x)
        {
            double x_rad = Trig.DegreeToRadian(x);
            double y = Math.Cos(x_rad);
            return y;
        }

        private static double[] Calculate_a_prime(double[] a, double[] b)
        {
            var C_ab = new double[2];
            for (int i = 0; i < 2; i++)
            {
                C_ab[i] = Math.Sqrt(a[i] * a[i] + b[i] * b[i]); // eq. (2)
            }
            double C_ab_mean = (C_ab[0] + C_ab[1]) / 2; // eq. (3)

            double G = 0.5d * (1 - Math.Sqrt(Math.Pow(C_ab_mean, 7) / (Math.Pow(C_ab_mean, 7) + Math.Pow(25, 7)))); // eq. (4)

            var a_prime = new double[2];
            for (int i = 0; i < 2; i++)
            {
                a_prime[i] = (1 + G) * a[i]; // eq. (5)
            }
            return a_prime;
        }

        private static double[] Calculate_C_prime(double[] a_prime, double[] b)
        {
            var C_prime = new double[2];
            for (int i = 0; i < 2; i++)
            {
                C_prime[i] = Math.Sqrt(a_prime[i] * a_prime[i] + b[i] * b[i]); // eq. (6)
            }
            return C_prime;
        }

        private static double[] Calculate_h_prime(double[] a_prime, double[] b)
        {
            // eq. (7)
            var h_prime = new double[2];
            for (int i = 0; i < 2; i++)
            {
                if (b[i] == 0d && a_prime[i] == 0d)
                    h_prime[i] = 0;

                double hRadians = Math.Atan2(b[i], a_prime[i]);
                double hDegrees = Trig.RadianToDegree(hRadians);
                if (hDegrees < 0)
                    hDegrees += 360;

                h_prime[i] = hDegrees;
            }
            return h_prime;
        }

        private static double Calculate_dh_prime(double[] C_prime, double[] h_prime)
        {
            // eq. (10)
            if (C_prime[0] * C_prime[1] != 0d)
            {
                if (Math.Abs(h_prime[1] - h_prime[0]) <= 180)
                    return h_prime[1] - h_prime[0];

                if ((h_prime[1] - h_prime[0]) > 180)
                    return (h_prime[1] - h_prime[0]) - 360;

                if ((h_prime[1] - h_prime[0]) < -180)
                    return (h_prime[1] - h_prime[0]) + 360;
            }
            return 0;
        }

        private static double Calculate_h_prime_mean(double[] h_prime, double[] C_prime)
        {
            // eq. (14)
            if (C_prime[0] * C_prime[1] != 0d)
            {
                if (Math.Abs(h_prime[0] - h_prime[1]) <= 180)
                    return (h_prime[0] + h_prime[1]) / 2;

                if (Math.Abs(h_prime[0] - h_prime[1]) > 180 &&
                    (h_prime[0] + h_prime[1]) < 360)
                    return (h_prime[0] + h_prime[1] + 360) / 2;

                if (Math.Abs(h_prime[0] - h_prime[1]) > 180 &&
                    (h_prime[0] + h_prime[1]) >= 360)
                    return (h_prime[0] + h_prime[1] - 360) / 2;
            }
            return (h_prime[0] + h_prime[1]);
        }
    }
}