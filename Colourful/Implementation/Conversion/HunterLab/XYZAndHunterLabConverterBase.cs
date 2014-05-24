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

namespace Colourful.Implementation.Conversion
{
    public abstract class XYZAndHunterLabConverterBase
    {
        protected double ComputeKa(XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 175;

            double Ka = 100 * (175 / 198.04) * (whitePoint.X + whitePoint.Y);
            return Ka;
        }

        protected double ComputeKb(XYZColor whitePoint)
        {
            if (whitePoint == Illuminants.C)
                return 70;

            double Ka = 100 * (70 / 218.11) * (whitePoint.Y + whitePoint.Z);
            return Ka;
        }
    }
}