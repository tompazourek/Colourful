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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Difference
{
    /// <summary>
    /// Computes distance between two vectors in color space
    /// </summary>
    /// <typeparam name="TColor"></typeparam>
    public interface IColorDifference<in TColor>
    {
        /// <summary>
        /// Computes distance between color x and y.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        double ComputeDifference(TColor x, TColor y);
    }
}