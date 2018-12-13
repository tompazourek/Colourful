using System;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Rec. 709 companding function
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Rec._709
    /// </remarks>
    public sealed class Rec709Companding : ICompanding
    {
        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var L = V < 0.081 ? V / 4.5 : Math.Pow((V + 0.099) / 1.099, 1 / 0.45);
            return L;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var L = channel;
            var V = L < 0.018 ? 4.5 * L : 1.099 * Math.Pow(L, 0.45) - 0.099;
            return V;
        }
    }
}