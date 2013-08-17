using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Colors
{
    /// <summary>
    ///CIE xy chromaticity
    /// </summary>
    public struct ChromaticityCoordinates
    {
        public double x;
        public double y;

        public ChromaticityCoordinates(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}