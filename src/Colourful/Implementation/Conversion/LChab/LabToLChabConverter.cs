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
using System.Linq;
using System.Text;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor"/> to <see cref="LChabColor"/>.
    /// </summary>
    public class LabToLChabConverter : IColorConversion<LabColor, LChabColor>
    {
        public LChabColor Convert(LabColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            double L = input.L, a = input.a, b = input.b;
            var C = Math.Sqrt(a*a + b*b);
            var hRadians = Math.Atan2(b, a);
            var hDegrees = Angle.NormalizeDegree(Angle.RadianToDegree(hRadians));

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