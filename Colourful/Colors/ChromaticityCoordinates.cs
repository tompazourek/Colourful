using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Colors
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public struct ChromaticityCoordinates
    {
        public readonly double x;
        public readonly double y;

        public ChromaticityCoordinates(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}