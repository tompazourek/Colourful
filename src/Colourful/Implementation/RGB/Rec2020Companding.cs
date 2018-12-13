using System;

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
        private const double Alpha = 1.09929682680944;
        private const double Beta = 0.018053968510807;
        private const double InverseBeta = Beta * 4.5;

        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var L = V < InverseBeta ? V / 4.5 : Math.Pow((V + Alpha - 1.0) / Alpha, 1 / 0.45);
            return L;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var L = channel;
            var V = L < Beta ? 4.5 * L : Alpha * Math.Pow(L, 0.45) - (Alpha - 1.0);
            return V;
        }
    }
}