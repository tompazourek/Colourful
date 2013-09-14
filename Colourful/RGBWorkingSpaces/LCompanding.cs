using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.RGBWorkingSpaces
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
        private const double Kappa = 24389d / 27d;
        private const double Epsilon = 216d / 24389d;

        public double InverseCompanding(double channel)
        {
            double V = channel;
            double v = V <= 0.08 ? 100 * V / Kappa : Math.Pow((V + 0.16) / 1.16, 3);
            return v;
        }

        public double Companding(double channel)
        {
            double v = channel;
            double V = v <= Epsilon ? v * Kappa / 100d : Math.Pow(1.16 * v, 1 / 3d) - 0.16;
            return V;
        }
    }
}