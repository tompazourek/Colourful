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
using Colourful.Implementation;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// XYZ scaling (chromatic adaptation)
    /// </summary>
    /// <remarks>
    /// Chromatic adaptation matrix is taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class XYZScaling : ChromaticAdaptationBase
    {
        private readonly Matrix _matrix = MatrixFactory.CreateIdentity(3);

        protected override Matrix MA
        {
            get { return _matrix; }
        }
    }
}