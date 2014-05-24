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
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful
{
    /// <summary>
    /// Color represented as a vector in its color space
    /// </summary>
    public interface IColorVector
    {
        Vector<double> Vector { get; }
    }
}