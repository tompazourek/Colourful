using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Colourful.Colors;
using MathNet.Numerics;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from <see cref="LabColor"/> to <see cref="LChabColor"/>.
    /// </summary>
    public class LabToLChabConverter : IColorConverter<LabColor, LChabColor>
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
    }
}