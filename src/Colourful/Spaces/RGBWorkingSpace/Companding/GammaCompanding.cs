using System;
using System.Diagnostics.CodeAnalysis;

namespace Colourful.RGBWorkingSpace
{
    /// <summary>
    /// Gamma companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public class GammaCompanding : ICompanding, IEquatable<GammaCompanding>
    {
        /// <summary>
        /// Constructs with given gamma
        /// </summary>
        public GammaCompanding(in double gamma)
        {
            Gamma = gamma;
        }

        /// <summary>
        /// Gamma
        /// </summary>
        public double Gamma { get; }

        /// <inheritdoc />
        public double ConvertToLinear(in double nonLinearChannel)
        {
            var V = nonLinearChannel;
            var v = Math.Pow(V, Gamma);
            return v;
        }

        /// <inheritdoc />
        public double ConvertToNonLinear(in double linearChannel)
        {
            var v = linearChannel;
            var V = Math.Pow(v, 1 / Gamma);
            return V;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals(GammaCompanding other)
        {
            if (other == null)
                return false;

            return Gamma == other.Gamma;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is GammaCompanding other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => Gamma.GetHashCode();

        /// <inheritdoc cref="object" />
        public static bool operator ==(GammaCompanding left, GammaCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(GammaCompanding left, GammaCompanding right) => !Equals(left, right);
    }
}