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
    /// Pair of companding functions for <see cref="IRGBWorkingSpace"/>.
    /// Used for conversion to XYZ and backwards.
    /// See also: <seealso cref="IRGBWorkingSpace.Companding"/>
    /// </summary>
    public interface ICompanding
    {
        /// <summary>
        /// Companded channel is made linear with respect to the energy.
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </remarks>
        double InverseCompanding(double channel);

        /// <summary>
        /// Uncompanded channel (linear) is made nonlinear (depends on the RGB color system).
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
        /// </remarks>
        double Companding(double channel);
    }
}