using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    public class BradfordChromaticAdaptation : ChromaticAdaptationBase
    {
        public override Matrix<double> MA
        {
            get
            {
                return DenseMatrix.OfRows(3, 3, new[]
                    {
                        new[] { 0.8951000, 0.2664000, -0.1614000 },
                        new[] { -0.7502000, 1.7135000, 0.0367000 },
                        new[] { 0.0389000, -0.0685000, 1.0296000 },
                    });
            }
        }
    }
}