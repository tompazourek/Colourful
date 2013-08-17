using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    public abstract class InverseGammaCompandingBase : IRGBWorkingSpace
    {
        protected abstract double Gamma { get; }

        public double InverseCompanding(double channel)
        {
            // Inverse Gamma Companding
            double V = channel;
            double v = Math.Pow(V, Gamma);
            return v;
        }

        public abstract XYZColorBase ReferenceWhite { get; }
        public abstract RGBSystemChromacityCoordinates ChromacityCoordinates { get; }
    }
}