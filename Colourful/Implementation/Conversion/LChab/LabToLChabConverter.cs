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
using MathNet.Numerics;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor"/> to <see cref="LChabColor"/>.
    /// </summary>
    public class LabToLChabConverter : IColorConversion<LabColor, LChabColor>
    {
        public LChabColor Convert(LabColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            double L = input.L, a = input.a, b = input.b;
            double C = Math.Sqrt(a * a + b * b);
            double hRadians = Math.Atan2(b, a);
            double hDegrees = Trig.RadianToDegree(hRadians);

            if (hDegrees > 360)
                hDegrees -= 360;
            else if (hDegrees < 0)
                hDegrees += 360;

            var output = new LChabColor(L, C, hDegrees, input.WhitePoint);
            return output;
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public static bool operator ==(LabToLChabConverter left, LabToLChabConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LabToLChabConverter left, LabToLChabConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}