#region License

// Copyright (C) Tomáš Pažourek, 2016
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

namespace Colourful.Implementation.Conversion
{
    public abstract class XYZAndHunterLabConverterBase
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ka"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ka")]
        protected static double ComputeKa(XYZColor whitePoint)
        {
            if (whitePoint == null) throw new ArgumentNullException(nameof(whitePoint));

            if (whitePoint == Illuminants.C)
                return 175;

            var Ka = 100*(175/198.04)*(whitePoint.X + whitePoint.Y);
            return Ka;
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Kb")]
        protected static double ComputeKb(XYZColor whitePoint)
        {
            if (whitePoint == null) throw new ArgumentNullException(nameof(whitePoint));

            if (whitePoint == Illuminants.C)
                return 70;

            var Ka = 100*(70/218.11)*(whitePoint.Y + whitePoint.Z);
            return Ka;
        }
    }
}