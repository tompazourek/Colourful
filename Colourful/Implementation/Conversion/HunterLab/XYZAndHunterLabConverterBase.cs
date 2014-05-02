using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

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