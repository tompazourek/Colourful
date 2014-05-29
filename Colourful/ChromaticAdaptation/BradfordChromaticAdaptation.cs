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
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// Bradford chromatic adaptation
    /// </summary>
    /// <remarks>
    /// Chromatic adaptation matrix is taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class BradfordChromaticAdaptation : ChromaticAdaptationBase
    {
        private readonly Matrix _matrix = new []
            {
                new [] { 0.8951000, 0.2664000, -0.1614000 },
                new [] { -0.7502000, 1.7135000, 0.0367000 },
                new [] { 0.0389000, -0.0685000, 1.0296000 },
            };

        protected override Matrix MA
        {
            get { return _matrix; }
        }
    }
}