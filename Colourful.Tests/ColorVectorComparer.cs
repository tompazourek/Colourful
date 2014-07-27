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

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two color vectors
    /// </summary>
    public class ColorVectorComparer : IComparer<IColorVector>
    {
        public ColorVectorComparer(IComparer<double> doubleComparer)
        {
            DoubleComparer = doubleComparer;
        }

        public IComparer<double> DoubleComparer { get; private set; }

        public int Compare(IColorVector x, IColorVector y)
        {
            IEnumerable<int> compared = x.Vector.Zip(y.Vector, DoubleComparer.Compare);
            int result = compared.FirstOrDefault(a => a != 0);
            return result;
        }
    }
}