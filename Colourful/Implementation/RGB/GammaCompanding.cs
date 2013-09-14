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
        public GammaCompanding(double gamma)
        {
            Gamma = gamma;
        }

        public double Gamma { get; private set; }

        public double InverseCompanding(double channel)
        {
            double V = channel;
            double v = Math.Pow(V, Gamma);
            return v;
        }

        public double Companding(double channel)
        {
            double v = channel;
            double V = Math.Pow(v, 1 / Gamma);
            return V;
        }

        public bool Equals(GammaCompanding other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return Gamma.Equals(other.Gamma);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GammaCompanding) obj);
        }

        public override int GetHashCode()
        {
            return Gamma.GetHashCode();
        }

        public static bool operator ==(GammaCompanding left, GammaCompanding right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GammaCompanding left, GammaCompanding right)
        {
            return !Equals(left, right);
        }
    }
}