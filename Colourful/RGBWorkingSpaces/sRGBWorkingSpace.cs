using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    //http://www.brucelindbloom.com/index.html?ColorCheckerCalcHelp.html
    public class sRGBWorkingSpace : IRGBWorkingSpace
    {
        private static readonly RGBSystemChromacityCoordinates ChromacityCoordinatesConst = new RGBSystemChromacityCoordinates
            {
                xr = 0.6400,
                yr = 0.3300,
                xg = 0.3000,
                yg = 0.6000,
                xb = 0.1500,
                yb = 0.0600,
            };

        public double InverseCompanding(double channel)
        {
            // Inverse sRGB Companding
            double V = channel;
            double v = V <= 0.04045 ? V / 12.92 : Math.Pow((V + 0.055) / 1.055, 2.4);
            return v;
        }

        public XYZColorBase ReferenceWhite
        {
            get { return Illuminants.D65; }
        }

        public RGBSystemChromacityCoordinates ChromacityCoordinates
        {
            get { return ChromacityCoordinatesConst; }
        }
    }
}