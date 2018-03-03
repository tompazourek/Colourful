using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "s")]
    public class sRGBCompanding : ICompanding
    {
        /// <inheritdoc />
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var v = V <= 0.04045 ? V/12.92 : Math.Pow((V + 0.055)/1.055, 2.4);
            return v;
        }

        /// <inheritdoc />
        public double Companding(double channel)
        {
            var v = channel;
            var V = v <= 0.0031308 ? 12.92*v : 1.055*Math.Pow(v, 1/2.4d) - 0.055;
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
        public static bool operator ==(sRGBCompanding left, sRGBCompanding right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(sRGBCompanding left, sRGBCompanding right)
        {
            return !Equals(left, right);
        }
    }
}