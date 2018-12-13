using System;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// L* companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public sealed class LCompanding : ICompanding
    {
        private const double Kappa = 24389d / 27d;
        private const double Epsilon = 216d / 24389d;

        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var v = V <= 0.08 ? 100.0 * V / Kappa : Math.Pow((V + 0.16) / 1.16, 3.0);
            return v;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var v = channel;
            var V = v <= Epsilon ? v * Kappa / 100.0 : 1.16 * Math.Pow(v, 1.0 / 3.0) - 0.16;
            return V;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is LCompanding;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LCompanding left, LCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LCompanding left, LCompanding right) => !Equals(left, right);
    }
}