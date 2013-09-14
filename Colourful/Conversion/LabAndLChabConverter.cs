using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using MathNet.Numerics;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from L*a*b* to L*C*h(ab) and backwards
    /// </summary>
    public class LabAndLChabConverter : IColorConverter<LabColor, LChabColor>, IColorConverter<LChabColor, LabColor>
    {
        public LChabColor Convert(LabColor input)
        {
            double L = input.L, a = input.a, b = input.b;
            double C = Math.Sqrt(a * a + b * b);
            double hRadians = Math.Atan2(b, a);
            double hDegrees = Trig.RadianToDegree(hRadians);
            
            if (hDegrees > 360)
                hDegrees -= 360;
            else if (hDegrees < 0)
                hDegrees += 360;

            var output = new LChabColor(L, C, hDegrees);
            return output;
        }

        public LabColor Convert(LChabColor input)
        {
            double L = input.L, C = input.C, hDegrees = input.h;
            double hRadians = Trig.DegreeToRadian(hDegrees);

            double a = C * Math.Cos(hRadians);
            double b = C * Math.Sin(hRadians);

            var output = new LabColor(L, a, b);
            return output;
        }
    }
}