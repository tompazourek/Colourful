using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful
{
    internal static class Extensions
    {
        private static readonly IComparer<double> DoubleComparer = new DoubleRoundingComparer(6);

        public static double CheckRange(this double value, double min, double max)
        {
            if (DoubleComparer.Compare(value, min) < 0)
                throw new ArgumentOutOfRangeException("value", value, "The minimum value is " + min);

            if (DoubleComparer.Compare(value, max) > 0)
                throw new ArgumentOutOfRangeException("value", value, "The maximum value is " + max);

            return value;
        }

        public static void AssignVariables(this Vector<double> vector, out double v1, out double v2, out double v3)
        {
            if (vector.Count != 3)
                throw new ArgumentOutOfRangeException("vector", "Vector must have 3 components.");

            v1 = vector[0];
            v2 = vector[1];
            v3 = vector[2];
        }
    }
}