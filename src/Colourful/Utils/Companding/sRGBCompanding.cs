using System;

namespace Colourful.Companding
{
    /// <summary>
    /// sRGB companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public class sRGBCompanding : ICompanding, IEquatable<sRGBCompanding>
    {
        /// <inheritdoc />
        public double ConvertToLinear(in double nonLinearChannel)
        {
            var V = nonLinearChannel;
            var v = V <= 0.04045 ? V / 12.92 : Math.Pow((V + 0.055) / 1.055, 2.4);
            return v;
        }

        /// <inheritdoc />
        public double ConvertToNonLinear(in double linearChannel)
        {
            var v = linearChannel;
            var V = v <= 0.0031308 ? 12.92 * v : 1.055 * Math.Pow(v, 1 / 2.4d) - 0.055;
            return V;
        }
        
        #region Equality

        /// <inheritdoc />
        public bool Equals(sRGBCompanding other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is sRGBCompanding;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(sRGBCompanding left, sRGBCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(sRGBCompanding left, sRGBCompanding right) => !Equals(left, right);

        #endregion
    }
}