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
    /// XYZ scaling (chromatic adaptation)
    /// </summary>
    /// <remarks>
    /// Chromatic adaptation matrix is taken from:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public class XYZScaling : ChromaticAdaptationBase
    {
        private readonly DiagonalMatrix _matrix = DiagonalMatrix.Identity(3);

        protected override Matrix<double> MA
        {
            get { return _matrix; }
        }
    }
}