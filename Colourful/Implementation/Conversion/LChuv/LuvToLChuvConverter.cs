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
    /// <summary>
    /// Converts from <see cref="LuvColor"/> to <see cref="LChuvColor"/>.
    /// </summary>
    public class LuvToLChuvConverter : IColorConversion<LuvColor, LChuvColor>
    {
        public LChuvColor Convert(LuvColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            double L = input.L, u = input.u, v = input.v;
            double C = Math.Sqrt(u * u + v * v);
            double hRadians = Math.Atan2(v, u);
            double hDegrees = Angle.RadianToDegree(hRadians);

            if (hDegrees > 360)
                hDegrees -= 360;
            else if (hDegrees < 0)
                hDegrees += 360;

            var output = new LChuvColor(L, C, hDegrees, input.WhitePoint);
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

        public static bool operator ==(LuvToLChuvConverter left, LuvToLChuvConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LuvToLChuvConverter left, LuvToLChuvConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}