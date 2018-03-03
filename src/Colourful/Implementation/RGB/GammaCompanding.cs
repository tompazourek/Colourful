using System;

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
    public class GammaCompanding : ICompanding
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
        public bool Equals(GammaCompanding other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Gamma.Equals(other.Gamma);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GammaCompanding)obj);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return Gamma.GetHashCode();
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(GammaCompanding left, GammaCompanding right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(GammaCompanding left, GammaCompanding right)
        {
            return !Equals(left, right);
        }
    }
}