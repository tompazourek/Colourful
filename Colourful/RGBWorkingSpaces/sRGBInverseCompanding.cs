using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Inverse sRGB companding
    /// </summary>
    /// <remarks>
    /// For more info see:
    /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
    /// </remarks>
    public class sRGBInverseCompanding : IInverseCompanding
    {
        public double InverseCompanding(double channel)
        {
            double V = channel;
            double v = V <= 0.04045 ? V / 12.92 : Math.Pow((V + 0.055) / 1.055, 2.4);
            return v;
        }
    }
}