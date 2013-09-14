using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Compares equality of two RGB working spaces
    /// </summary>
    public class RGBWorkingSpaceEqualityComparer : IEqualityComparer<IRGBWorkingSpace>
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly IEqualityComparer<IRGBWorkingSpace> Default = new RGBWorkingSpaceEqualityComparer();

        public bool Equals(IRGBWorkingSpace x, IRGBWorkingSpace y)
        {
            if (x == null || y == null)
                return EqualityComparer<IRGBWorkingSpace>.Default.Equals(x, y);

            if (!x.ReferenceWhite.Equals(y.ReferenceWhite))
                return false;

            if (!x.Companding.Equals(y.Companding))
                return false;

            if (!x.ChromaticityCoordinates.Equals(y.ChromaticityCoordinates))
                return false;

            return true;
        }

        public int GetHashCode(IRGBWorkingSpace obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            unchecked
            {
                int hashCode = obj.ReferenceWhite.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Companding.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.ChromaticityCoordinates.GetHashCode();
                return hashCode;
            }
        }
    }
}