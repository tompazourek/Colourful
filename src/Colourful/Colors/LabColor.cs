﻿#region License

// Copyright (C) Tomáš Pažourek, 2016
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
#if (!READONLYCOLLECTIONS)
using Vector = System.Collections.Generic.IList<double>;
using Matrix = System.Collections.Generic.IList<System.Collections.Generic.IList<double>>;

#else
using Vector = System.Collections.Generic.IReadOnlyList<double>;
using Matrix = System.Collections.Generic.IReadOnlyList<System.Collections.Generic.IReadOnlyList<double>>;
#endif

namespace Colourful
{
    /// <summary>
    /// CIE L*a*b* (1976) color
    /// </summary>
    public class LabColor : IColorVector
    {
        /// <summary>
        /// D50 standard illuminant.
        /// Used when reference white is not specified explicitly.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly XYZColor DefaultWhitePoint = Illuminants.D50;

        #region Constructor

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="a">a* (usually from -100 to 100)</param>
        /// <param name="b">b* (usually from -100 to 100)</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LabColor(double l, double a, double b) : this(l, a, b, DefaultWhitePoint)
        {
        }

        /// <param name="l">L* (lightness) (from 0 to 100)</param>
        /// <param name="a">a* (usually from -100 to 100)</param>
        /// <param name="b">b* (usually from -100 to 100)</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "l")]
        public LabColor(double l, double a, double b, XYZColor whitePoint)
        {
            L = l;
            this.a = a;
            this.b = b;
            WhitePoint = whitePoint;
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <remarks>Uses <see cref="DefaultWhitePoint"/> as white point.</remarks>
        public LabColor(Vector vector) : this(vector, DefaultWhitePoint)
        {
        }

        /// <param name="vector"><see cref="Vector"/>, expected 3 dimensions</param>
        /// <param name="whitePoint">Reference white (see <see cref="Illuminants"/>)</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public LabColor(Vector vector, XYZColor whitePoint)
            : this(vector[0], vector[1], vector[2], whitePoint)
        {
        }

        #endregion

        #region Channels

        /// <summary>
        /// L* (lightness)
        /// </summary>
        /// <remarks>
        /// Ranges from 0 to 100.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L")]
        public double L { get; }

        /// <summary>
        /// a*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate green while positive values indicate magenta.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "a"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public double a { get; }

        /// <summary>
        /// b*
        /// </summary>
        /// <remarks>
        /// Ranges usually from -100 to 100.
        /// Negative values indicate blue and positive values indicate yellow.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b")]
        public double b { get; }

        /// <remarks><see cref="Illuminants"/></remarks>
        public XYZColor WhitePoint { get; private set; }

        /// <summary>
        /// <see cref="IColorVector"/>
        /// </summary>
        public Vector Vector => new[] { L, a, b };

        #endregion

        #region Equality

        public bool Equals(LabColor other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return L.Equals(other.L) && a.Equals(other.a) && b.Equals(other.b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabColor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = L.GetHashCode();
                hashCode = (hashCode*397) ^ a.GetHashCode();
                hashCode = (hashCode*397) ^ b.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(LabColor left, LabColor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LabColor left, LabColor right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "Lab [L={0:0.##}, a={1:0.##}, b={2:0.##}]", L, a, b);
        }

        #endregion
    }
}