using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.RGBWorkingSpaces
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
        public double Gamma { get; private set; }

        public GammaCompanding(double gamma)
        {
            Gamma = gamma;
        }

        public double InverseCompanding(double channel)
        {
            double V = channel;
            double v = Math.Pow(V, Gamma);
            return v;
        }

        public double Companding(double channel)
        {
            double v = channel;
            double V = Math.Pow(v, 1/Gamma);
            return V;
        }
    }
}