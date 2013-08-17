using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    public class VonKriesChromaticAdaptation : ChromaticAdaptationBase
    {
        public override Matrix<double> MA
        {
            get
            {
                return DenseMatrix.OfRows(3, 3, new[]
                    {
                        new[] { 0.4002400, 0.7076000, -0.0808100 },
                        new[] { -0.2263000, 1.1653200, 0.0457000 },
                        new[] { 0.0000000, 0.0000000, 0.9182200 },
                    });
            }
        }
    }
}