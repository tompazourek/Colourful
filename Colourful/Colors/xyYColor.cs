using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// CIE xyY color space (derived from <see cref="XYZColor"/> color space)
    /// </summary>
    public class xyYColor : IColorVector
    {
        #region Constructor

        /// <param name="x">x (usually from 0 to 1) chromaticity coordinate</param>
        /// <param name="y">y (usually from 0 to 1) chromaticity coordinate</param>
        /// <param name="Y">Y (usually from 0 to 1)</param>
        public xyYColor(double x, double y, double Y)
            : this(new ChromaticityCoordinates(x, y), Y)
        {
        }

        /// <param name="chromaticity">Chromaticity coordinates (x and y together)</param>
        /// <param name="Y">Y (usually from 0 to 1)</param>
        public xyYColor(ChromaticityCoordinates chromaticity, double Y)
        {
            if (chromaticity == null)
                throw new ArgumentNullException("chromaticity");

            Chromaticity = chromaticity;
            this.Y = Y;
        }

        #endregion

        #region Channels

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double x { get { return Chromaticity.x; } }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double y
        {
            get { return Chromaticity.y; }
        }

        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public double Y { get; private set; }

        /// <remarks>
        /// Chromaticity coordinates (identical to x and y)
        /// </remarks>
        public ChromaticityCoordinates Chromaticity { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { x, y, Y }); }
        }

        #endregion

        #region Equality

        public bool Equals(xyYColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return x.Equals(other.x) && y.Equals(other.y) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((xyYColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(xyYColor left, xyYColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(xyYColor left, xyYColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "xyY [x={0:0.##}, y={1:0.##}, Y={2:0.##}]", x, y, Y);
        }

        #endregion
    }
}