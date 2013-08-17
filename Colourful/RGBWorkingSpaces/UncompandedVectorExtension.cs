using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.RGBWorkingSpaces
{
    public static class UncompandedVectorExtension
    {
        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.InverseCompanding"/>) to RGB vector.
        /// </summary>
        /// <param name="rgbColor"></param>
        /// <returns></returns>
        public static Vector<double> GetUncompandedVector(this RGBColor rgbColor)
        {
            Func<double, double> inverseCompandingFunction = rgbColor.WorkingSpace.InverseCompanding;
            Vector<double> compandedVector = rgbColor.Vector;
            DenseVector uncompandedVector = DenseVector.OfEnumerable(compandedVector.Select(inverseCompandingFunction));
            return uncompandedVector;
        }
    }
}