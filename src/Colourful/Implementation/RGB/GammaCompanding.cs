using System;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Gamma companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public sealed class GammaCompanding : ICompanding
    {
        /// <summary>
        /// Constructs with given gamma
        /// </summary>
        public GammaCompanding(double gamma)
        {
            Gamma = gamma;
        }

        /// <summary>
        /// Gamma
        /// </summary>
        public double Gamma { get; }

        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var v = Math.Pow(V, Gamma);
            return v;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var v = channel;
            var V = Math.Pow(v, 1 / Gamma);
            return V;
        }

        /// <inheritdoc cref="object" />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(GammaCompanding other)
        {
            if (other == null)
                return false;

            return Gamma == other.Gamma;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is GammaCompanding other && Equals(other);

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => Gamma.GetHashCode();

        /// <inheritdoc cref="object" />
        public static bool operator ==(GammaCompanding left, GammaCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(GammaCompanding left, GammaCompanding right) => !Equals(left, right);
    }
}