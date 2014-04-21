using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Colors
{
    /// <summary>
    /// RGB color without specified <see cref="IRGBWorkingSpace">working space</see>
    /// </summary>
    public abstract class RGBColorBase : IColorVector
    {
        #region Constructor

        /// <param name="r">Red (from 0 to 1)</param>
        /// <param name="g">Green (from 0 to 1)</param>
        /// <param name="b">Blue (from 0 to 1)</param>
        internal RGBColorBase(double r, double g, double b)
        {
            R = r.CheckRange(0, 1);
            G = g.CheckRange(0, 1);
            B = b.CheckRange(0, 1);
        }

        #endregion

        #region Channels

        /// <summary>
        /// Red
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        public double R { get; private set; }

        /// <summary>
        /// Green
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G")]
        public double G { get; private set; }

        /// <summary>
        /// Blue
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        public double B { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector<double> Vector
        {
            get { return DenseVector.OfEnumerable(new[] { R, G, B }); }
        }

        #endregion

        #region Equality

        protected bool Equals(RGBColorBase other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RGBColorBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RGBColorBase left, RGBColorBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RGBColorBase left, RGBColorBase right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}