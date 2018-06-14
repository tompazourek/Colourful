﻿using System;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Rec. 2020 companding function (for 12-bit).
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Rec._2020
    /// For 10-bits, companding is identical to <see cref="Colourful.Implementation.RGB.Rec709Companding" />
    /// </remarks>
    public sealed class Rec2020Companding : ICompanding
    {
        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var L = V < 0.08145 ? V / 4.5 : Math.Pow((V + 0.0993) / 1.0993, 1 / 0.45);
            return L;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var L = channel;
            var V = L < 0.0181 ? 4500 * L : 1.0993 * L - 0.0993;
            return V;
        }
    }
}