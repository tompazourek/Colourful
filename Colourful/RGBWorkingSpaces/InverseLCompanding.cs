using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Inverse L* companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// </remarks>
    public class InverseLCompanding : IInverseCompanding
    {
        public double InverseCompanding(double channel)
        {
            double V = channel;
            const double kappa = 24389d / 27d;
            double v = V <= 0.08 ? 100 * V / kappa : Math.Pow((V + 0.16) / 1.16, 3);
            return v;
        }
    }
}