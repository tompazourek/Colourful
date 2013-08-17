using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// The base of all colors in color spaces
    /// </summary>
    public interface IColorVector
    {
        Vector<double> Vector { get; }
    }
}