using System;
using System.Collections.Generic;
using System.Linq;

namespace Colourful.Tests
{
    /// <summary>
    /// Compares two color vectors.
    /// </summary>
    public class ColorVectorComparer : IComparer<IColorVector>, IEqualityComparer<IColorVector>
    {
        public ColorVectorComparer(IComparer<double> doubleComparer)
        {
            DoubleComparer = doubleComparer;
        }

        public IComparer<double> DoubleComparer { get; }

        public int Compare(IColorVector x, IColorVector y)
        {
            if (x == null)
                return y == null ? 0 : 1;

            if (y == null)
                return -1;

            var compared = x.Vector.Zip(y.Vector, DoubleComparer.Compare);
            var result = compared.FirstOrDefault(a => a != 0);
            return result;
        }

        public bool Equals(IColorVector x, IColorVector y) => Compare(x, y) == 0;

        public int GetHashCode(IColorVector obj) => throw new NotSupportedException();
    }
}