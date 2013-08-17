using System;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    public abstract class InverseGammaCompandingBase : IRGBWorkingSpace
    {
        public double InverseCompanding(double channel)
        {
            // Inverse Gamma Companding
            var V = channel;
            var v = Math.Pow(V, Gamma);
            return v;
        }

        public abstract XYZColorBase ReferenceWhite { get; }
        public abstract RGBSystemChromacityCoordinates ChromacityCoordinates { get; }

        protected abstract double Gamma { get; }
    }
}
