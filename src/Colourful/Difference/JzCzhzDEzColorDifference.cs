using static System.Math;
using static Colourful.Internals.MathUtils;

namespace Colourful
{
    /// <summary>
    /// Delta Ez color difference for JzCzhz color space.
    /// </summary>
    public sealed class JzCzhzDEzColorDifference : IColorDifference<JzCzhzColor>
    {
        /// <inheritdoc />
        public double ComputeDifference(in JzCzhzColor x, in JzCzhzColor y)
        {
            // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

            var dJz = y.Jz - x.Jz;
            var dCz = y.Cz - x.Cz;
            var dhz = DegreeToRadian(y.hz) - DegreeToRadian(x.hz);
            var dHz2 = 2 * x.Cz * y.Cz * (1 - Cos(dhz));

            var result = Sqrt(dJz * dJz + dCz * dCz + dHz2);
            return result;
        }
    }
}
