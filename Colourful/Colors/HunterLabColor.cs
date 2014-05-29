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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector = System.Collections.Generic.IReadOnlyList<double>;

namespace Colourful
{
    /// <summary>
    /// Hunter Lab color
    /// </summary>
    public class HunterLabColor : IColorVector
    {
        /// <summary>
        /// C standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.C;

        #region Constructor

        /// <param name="l">L (lightness)</param>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public HunterLabColor(double l, double a, double b) : this(l, a, b, DefaultWhitePoint)
        {
        }

        /// <param name="l">L (lightness)</param>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public HunterLabColor(double l, double a, double b, XYZColor whitePoint)
        {
            L = l;
            this.a = a;
            this.b = b;
            WhitePoint = whitePoint;
        }

        #endregion

        #region Channels

        /// <summary>
        /// L (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L")]
        public double L { get; private set; }

        /// <summary>
        /// a
        /// </summary>
        /// <remarks>
        /// Negative values indicate green while positive values indicate magenta.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public double a { get; private set; }

        /// <summary>
        /// b
        /// </summary>
        /// <remarks>
        /// Negative values indicate blue and positive values indicate yellow.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
        public double b { get; private set; }

        /// <remarks><see cref="Illuminants"/></remarks>
        public XYZColor WhitePoint { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector
        {
            get { return new[] { L, a, b }; }
        }

        #endregion

        #region Equality

        public bool Equals(LabColor other)
        {
            if (other == null) throw new ArgumentNullException("other");
            return L.Equals(other.L) && a.Equals(other.a) && b.Equals(other.b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabColor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = L.GetHashCode();
                hashCode = (hashCode * 397) ^ a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(HunterLabColor left, HunterLabColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HunterLabColor left, HunterLabColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "HunterLab [L={0:0.##}, a={1:0.##}, b={2:0.##}]", L, a, b);
        }

        #endregion
    }
}