using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Difference
{
    /// <summary>
    /// CIE Delta-E 1976 formula
    /// </summary>
    public class CIE76ColorDifference : IColorDifference<LabColor>
    {
        /// <param name="x">Reference color</param>
        /// <param name="y">Sample color</param>
        /// <returns>Delta-E (1976) color difference</returns>
        public double ComputeDifference(LabColor x, LabColor y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");

            var distance = (x.Vector - y.Vector).Norm(2);
            return distance;
        }
    }
}