using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    public abstract class InverseLCompandingBase : IRGBWorkingSpace
    {
        public double InverseCompanding(double channel)
        {
            // Inverse L* Companding
            var V = channel;
            const double kappa = 24389d/27d;
            var v = V <= 0.08 ? 100*V/kappa : Math.Pow((V + 0.16)/1.16, 3);
            return v;
        }

        public abstract XYZColorBase ReferenceWhite { get; }
        public abstract RGBSystemChromacityCoordinates ChromacityCoordinates { get; }
    }
}
