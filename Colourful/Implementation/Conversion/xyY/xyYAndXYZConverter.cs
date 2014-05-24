using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Implementation.Conversion
{
    public class xyYAndXYZConverter : IColorConversion<XYZColor, xyYColor>, IColorConversion<xyYColor, XYZColor>
    {
        public XYZColor Convert(xyYColor input)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (input.y == 0)
                return new XYZColor(0, 0, input.Y);
            // ReSharper restore CompareOfFloatsByEqualityOperator

            double X = (input.x * input.Y) / input.y;
            double Y = input.Y;
            double Z = ((1 - input.x - input.y) * Y) / input.y;

            return new XYZColor(X, Y, Z);
        }

        public xyYColor Convert(XYZColor input)
        {
            double x = input.X / (input.X + input.Y + input.Z);
            double y = input.Y / (input.X + input.Y + input.Z);

            if (double.IsNaN(x) || double.IsNaN(y))
                return new xyYColor(0, 0, input.Y);

            double Y = input.Y;
            return new xyYColor(x, y, Y);
        }
    }
}