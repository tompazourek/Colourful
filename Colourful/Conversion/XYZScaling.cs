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
    public class XYZScaling : ChromaticAdaptationBase
    {
        public override Matrix<double> MA
        {
            get { return DiagonalMatrix.Identity(3); }
        }
    }
}