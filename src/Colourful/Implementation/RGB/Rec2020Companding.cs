using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Rec. 2020 companding function (for 12-bit).
    /// </summary>
    /// <remarks>
    /// http://en.wikipedia.org/wiki/Rec._2020
    /// 
    /// For 10-bits, companding is identical to <see cref="Colourful.Implementation.RGB.Rec709Companding"/>
    /// </remarks>
    public class Rec2020Companding : ICompanding
    {
        public double InverseCompanding(double channel)
        {
            var V = channel;
            var L = V < 0.08145 ? V/4.5 : Math.Pow((V + 0.0993)/1.0993, 1/0.45);
            return L;
        }

        public double Companding(double channel)
        {
            var L = channel;
            var V = L < 0.0181 ? 4500*L : 1.0993*L - 0.0993;
            return V;
        }
    }
}