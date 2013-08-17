using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.RGBWorkingSpaces;

namespace Colourful
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new RGBColor(0.694617, 0.173810, 0.218710);
            var xyz1 = c.ToXYZ(Illuminants.D65);
            var xyz2 = c.ToXYZ(Illuminants.D50);

        }
    }
}
