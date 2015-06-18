#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Globalization;

#if (NET40 || NET35)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;
#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful
{
    /// <summary>
    /// LMS color space represented by the response of the three types of cones of the human eye
    /// </summary>
    public class LMSColor : IColorVector
    {
        #region Constructor

        /// <param name="l">L (usually from -1 to 1)</param>
        /// <param name="m">M (usually from -1 to 1)</param>
        /// <param name="s">S (usually from -1 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "m"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public LMSColor(double l, double m, double s)
        {
            L = l;
            M = m;
            S = s;
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public LMSColor(Vector vector)
            : this(vector[0], vector[1], vector[2])
        {
        }

        #endregion

        #region Channels

       
        /// <summary>
        /// Long wavelengths (red) cone response (Rho)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L")]
        public double L { get; private set; }

        /// <summary>
        /// Medium wavelengths (green) cone response (Gamma)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        public double M { get; private set; }

        /// <summary>
        /// Short wavelengths (blue) cone response (Beta)
        /// </summary>
        /// <remarks>
        /// Ranges usually from -1 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "S")]
        public double S { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector
        {
            get { return new[] { L, M, S }; }
        }

        #endregion

        #region Equality

        public bool Equals(LMSColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return L.Equals(other.L) && M.Equals(other.M) && S.Equals(other.S);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LMSColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ M.GetHashCode();
                hashCode = (hashCode * 397) ^ S.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LMSColor left, LMSColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LMSColor left, LMSColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "LMS [L={0:0.##}, M={1:0.##}, S={2:0.##}]", L, M, S);
        }

        #endregion
    }
}