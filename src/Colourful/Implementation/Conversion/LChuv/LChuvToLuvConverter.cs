using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChuvColor"/> to <see cref="LuvColor"/>.
    /// </summary>
    public class LChuvToLuvConverter : IColorConversion<LChuvColor, LuvColor>
    {
        public LuvColor Convert(LChuvColor input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(hDegrees);

            var u = C*Math.Cos(hRadians);
            var v = C*Math.Sin(hRadians);

            var output = new LuvColor(L, u, v, input.WhitePoint);
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

        public static bool operator ==(LChuvToLuvConverter left, LChuvToLuvConverter right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LChuvToLuvConverter left, LChuvToLuvConverter right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}