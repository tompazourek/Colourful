using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Colors
{
    // http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    public static class Illuminants
    {
        public static XYZColorBase A = new XYZColorBase(1.09850, 1, 0.35585);
        public static XYZColorBase B = new XYZColorBase(0.99072, 1, 0.85223);
        public static XYZColorBase C = new XYZColorBase(0.98074, 1, 1.18232);
        public static XYZColorBase D50 = new XYZColorBase(0.96422, 1, 0.82521);
        public static XYZColorBase D55 = new XYZColorBase(0.95682, 1, 0.92149);
        public static XYZColorBase D65 = new XYZColorBase(0.95047, 1, 1.08883);
        public static XYZColorBase D75 = new XYZColorBase(0.94972, 1, 1.22638);
        public static XYZColorBase E = new XYZColorBase(1, 1, 1);
        public static XYZColorBase F2 = new XYZColorBase(0.99186, 1, 0.67393);
        public static XYZColorBase F7 = new XYZColorBase(0.95041, 1, 1.08747);
        public static XYZColorBase F11 = new XYZColorBase(1.00962, 1, 0.64350);
    }
}