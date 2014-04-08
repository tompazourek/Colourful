using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// Von Kries chromatic adaptation
    /// </summary>
    /// <remarks>
    /// Chromatic adaptation matrix is taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class VonKriesChromaticAdaptation : ChromaticAdaptationBase
    {
        private readonly DenseMatrix _matrix = DenseMatrix.OfRows(3, 3, new[]
            {
                new[] { 0.4002400, 0.7076000, -0.0808100 },
                new[] { -0.2263000, 1.1653200, 0.0457000 },
                new[] { 0.0000000, 0.0000000, 0.9182200 },
            });

        protected override Matrix<double> MA
        {
            get
            {
                return _matrix;
            }
        }
    }
}