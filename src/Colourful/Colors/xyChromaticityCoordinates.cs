#region License

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

namespace Colourful
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "xy"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "xy")]
    public struct xyChromaticityCoordinates
    {
        /// <param name="x">Chromaticity coordinate x (usually from 0 to 1)</param>
        /// <param name="y">Chromaticity coordinate y (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public xyChromaticityCoordinates(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Chromaticity coordinate x
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public double x { get; }

        /// <summary>
        /// Chromaticity coordinate y
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public double y { get; }

        public bool Equals(xyChromaticityCoordinates other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is xyChromaticityCoordinates && Equals((xyChromaticityCoordinates)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode()*397) ^ y.GetHashCode();
            }
        }

        public static bool operator ==(xyChromaticityCoordinates left, xyChromaticityCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(xyChromaticityCoordinates left, xyChromaticityCoordinates right)
        {
            return !left.Equals(right);
        }

        #region Overrides

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);
        }

        #endregion
    }
}