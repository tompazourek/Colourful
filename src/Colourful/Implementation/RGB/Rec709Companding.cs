using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Rec. 709 companding function
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Rec._709
    /// </remarks>
    public class Rec709Companding : ICompanding
    {
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var L = V < 0.081 ? V/4.5 : Math.Pow((V + 0.099)/1.099, 1/0.45);
            return L;
        }

        public double Companding(double channel)
        {
            var L = channel;
            var V = L < 0.018 ? 4500*L : 1.099*L - 0.099;
            return V;
        }
    }
}