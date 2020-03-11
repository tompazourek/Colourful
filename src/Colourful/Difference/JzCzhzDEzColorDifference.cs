using System;
using Colourful.Implementation;

namespace Colourful.Difference
{
    /// <summary>
    /// Delta Ez color difference for JzCzhz color space.
    /// </summary>
    /// <remarks>
    /// Equations: http://www.brucelindbloom.com/index.html?Eqn_DeltaE_CMC.html
    /// </remarks>
    public sealed class JzCzhzDEzColorDifference : IColorDifference<JzCzhzColor>
    {
        /// <inheritdoc />
        public double ComputeDifference(in JzCzhzColor x, in JzCzhzColor y)
        {
            // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

            var dJz = y.Jz - x.Jz;
            var dCz = y.Cz - x.Cz;
            var dhz = Angle.DegreeToRadian(y.hz) - Angle.DegreeToRadian(x.hz);
            var dHz2 = 2 * x.Cz * y.Cz * (1 - Math.Cos(dhz));

            var result = Math.Sqrt(dJz * dJz + dCz * dCz + dHz2);
            return result;
        }
    }
}