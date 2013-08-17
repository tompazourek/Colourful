using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Inverse gamma companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// </remarks>
    public class InverseGammaCompanding : IInverseCompanding
    {
        public double Gamma { get; private set;}

        public InverseGammaCompanding(double gamma)
        {
            Gamma = gamma;
        }

        public double InverseCompanding(double channel)
        {
            // inverse gamma companding
            double V = channel;
            double v = Math.Pow(V, Gamma);
            return v;
        }
    }
}