using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    public interface IColorVector
    {
        Vector<double> Vector { get; }
    }
}
