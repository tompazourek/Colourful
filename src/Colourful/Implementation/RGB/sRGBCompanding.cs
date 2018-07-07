using System;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// sRGB companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
    /// </remarks>
    public sealed class sRGBCompanding : ICompanding
    {
        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var v = V <= 0.04045 ? V / 12.92 : Math.Pow((V + 0.055) / 1.055, 2.4);
            return v;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var v = channel;
            var V = v <= 0.0031308 ? 12.92 * v : 1.055 * Math.Pow(v, 1 / 2.4d) - 0.055;
            return V;
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is sRGBCompanding;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(sRGBCompanding left, sRGBCompanding right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(sRGBCompanding left, sRGBCompanding right) => !Equals(left, right);
    }
}