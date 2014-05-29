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
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// Von Kries chromatic adaptation
    /// </summary>
    /// <remarks>
    /// Chromatic adaptation matrix is taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class VonKriesChromaticAdaptation : ChromaticAdaptationBase
    {
        private readonly Matrix _matrix = new[]
            {
                new[] { 0.4002400, 0.7076000, -0.0808100 },
                new[] { -0.2263000, 1.1653200, 0.0457000 },
                new[] { 0.0000000, 0.0000000, 0.9182200 },
            };

        protected override Matrix MA
        {
            get { return _matrix; }
        }
    }
}