using System;
using static System.Math;

namespace Colourful.Internals
{
    /// <summary>
    /// L* companding.
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public class LCompanding : ICompanding, IEquatable<LCompanding>
    {
        private const double Kappa = 24389d / 27d;
        private const double Epsilon = 216d / 24389d;

        /// <inheritdoc />
        public double ConvertToLinear(in double nonLinearChannel)
        {
            var V = nonLinearChannel;
            var v = V <= 0.08 ? 100.0 * V / Kappa : Pow((V + 0.16) / 1.16, y: 3.0);
            return v;
        }

        /// <inheritdoc />
        public double ConvertToNonLinear(in double linearChannel)
        {
            var v = linearChannel;
            var V = v <= Epsilon ? v * Kappa / 100.0 : 1.16 * Pow(v, 1.0 / 3.0) - 0.16;
            return V;
        }

        #region Equality

        /// <inheritdoc />
        public bool Equals(LCompanding other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LCompanding;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LCompanding left, LCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LCompanding left, LCompanding right) => !Equals(left, right);

        #endregion
    }
}