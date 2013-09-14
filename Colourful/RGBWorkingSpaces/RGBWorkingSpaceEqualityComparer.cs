using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Compares equality of two RGB working spaces
    /// </summary>
    public class RGBWorkingSpaceEqualityComparer : IEqualityComparer<IRGBWorkingSpace>
    {
        public static readonly IEqualityComparer<IRGBWorkingSpace> Default = new RGBWorkingSpaceEqualityComparer();

        public bool Equals(IRGBWorkingSpace x, IRGBWorkingSpace y)
        {
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