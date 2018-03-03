using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
    public class LCompanding : ICompanding
    {
        private const double Kappa = 24389d/27d;
        private const double Epsilon = 216d/24389d;

        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var v = V <= 0.08 ? 100*V/Kappa : MathUtils.Pow3((V + 0.16)/1.16);
            return v;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var v = channel;
            var V = v <= Epsilon ? v*Kappa/100d : Math.Pow(1.16*v, 1/3d) - 0.16;
            return V;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return true;
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return 1;
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(LCompanding left, LCompanding right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(LCompanding left, LCompanding right)
        {
            return !Equals(left, right);
        }
    }
}