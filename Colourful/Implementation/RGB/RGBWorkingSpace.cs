using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Trivial implementation of <see cref="IRGBWorkingSpace"/>
    /// </summary>
    public class RGBWorkingSpace : IRGBWorkingSpace
    {
        public RGBWorkingSpace(XYZColor referenceWhite, ICompanding companding, RGBPrimariesChromaticityCoordinates chromaticityCoordinates)
        {
            WhitePoint = referenceWhite;
            Companding = companding;
            ChromaticityCoordinates = chromaticityCoordinates;
        }

        public XYZColor WhitePoint { get; private set; }

        public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates { get; private set; }

        public ICompanding Companding { get; private set; }

        #region Overrides

        protected bool Equals(IRGBWorkingSpace other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return Equals(WhitePoint, other.WhitePoint) && ChromaticityCoordinates.Equals(other.ChromaticityCoordinates) && Equals(Companding, other.Companding);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var workingSpace = obj as IRGBWorkingSpace;
            if (workingSpace == null) return false;
            return Equals(workingSpace);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (WhitePoint != null ? WhitePoint.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ChromaticityCoordinates.GetHashCode();
                hashCode = (hashCode * 397) ^ (Companding != null ? Companding.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(RGBWorkingSpace left, RGBWorkingSpace right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RGBWorkingSpace left, RGBWorkingSpace right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}