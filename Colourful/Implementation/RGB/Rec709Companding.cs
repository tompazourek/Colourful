#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            double V = channel;
            double L = V < 0.081 ? V / 4.5 : Math.Pow((V + 0.099) / 1.099, 1 / 0.45);
            return L;
        }

        public double Companding(double channel)
        {
            double L = channel;
            double V = L < 0.018 ? 4500 * L : 1.099 * L - 0.099;
            return V;
        }
    }
}