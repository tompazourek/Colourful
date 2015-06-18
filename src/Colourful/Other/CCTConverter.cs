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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Colourful
{
    /// <summary>
    /// Can compute chromaticity from CCT (Correlated color temperature) and also
    /// compute CCT of given chromaticity.
    /// </summary>
    public class CCTConverter
    {
        /// <summary>
        /// Returns chromaticity coordinates of given CCT (specified in K)
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public xyChromaticityCoordinates GetChromaticityOfCCT(double temperature)
        {
            // approximation described here: http://en.wikipedia.org/wiki/Planckian_locus#Approximation

            double x_c;

            if (temperature <= 4000) // correctly 1667 <= T <= 4000
                x_c = -0.2661239 * (Math.Pow(10, 9) / Math.Pow(temperature, 3)) - 0.2343580 * (Math.Pow(10, 6) / Math.Pow(temperature, 2)) + 0.8776956 * (Math.Pow(10, 3) / temperature) + 0.179910;

            else // correctly 4000 <= T <= 25000
                x_c = -3.0258469 * (Math.Pow(10, 9) / Math.Pow(temperature, 3)) + 2.1070379 * (Math.Pow(10, 6) / Math.Pow(temperature, 2)) + 0.2226347 * (Math.Pow(10, 3) / temperature) + 0.240390;

            double y_c;

            if (temperature <= 2222) // correctly 1667 <= T <= 2222
                y_c = - 1.1063814 * Math.Pow(x_c, 3) - 1.34811020 * Math.Pow(x_c, 2) + 2.18555832 * x_c - 0.20219683;

            else if (temperature <= 4000) // correctly 2222 <= T <= 4000
                y_c = -0.9549476 * Math.Pow(x_c, 3) - 1.37418593 * Math.Pow(x_c, 2) + 2.09137015 * x_c - 0.16748867;

            else // correctly 4000 <= T <= 25000
                y_c = +3.0817580 * Math.Pow(x_c, 3) - 5.87338670 * Math.Pow(x_c, 2) + 3.75112997 * x_c - 0.37001483;

            return new xyChromaticityCoordinates(x_c, y_c);
        }

        /// <summary>
        /// Returns CCT (specified in K) of given chromaticity coordinates
        /// </summary>
        /// <remarks>Ranges usually from around 0 to 25000</remarks>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public double GetCCTOfChromaticity(xyChromaticityCoordinates chromaticity)
        {
            // approximation described here: http://en.wikipedia.org/wiki/Color_temperature#Approximation

            const double xe = 0.3366;
            const double ye = 0.1735;
            const double A0 = -949.86315;
            const double A1 = 6253.80338;
            const double t1 = 0.92159;
            const double A2 = 28.70599;
            const double t2 = 0.20039;
            const double A3 = 0.00004;
            const double t3 = 0.07125;
            double n = (chromaticity.x - xe) / (chromaticity.y - ye);
            double cct = A0 + A1 * Math.Exp(-n / t1) + A2 * Math.Exp(-n / t2) + A3 * Math.Exp(-n / t3);
            return cct;
        }
    }
}