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

namespace Colourful
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public struct ChromaticityCoordinates
    {
        private readonly double _x;
        private readonly double _y;

        /// <param name="x">Chromaticity coordinate x (usually from 0 to 1)</param>
        /// <param name="y">Chromaticity coordinate y (usually from 0 to 1)</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public ChromaticityCoordinates(double x, double y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Chromaticity coordinate x
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public double x
        {
            get { return _x; }
        }

        /// <summary>
        /// Chromaticity coordinate y
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "y"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public double y
        {
            get { return _y; }
        }

        public bool Equals(ChromaticityCoordinates other)
        {
            return _x.Equals(other._x) && _y.Equals(other._y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ChromaticityCoordinates && Equals((ChromaticityCoordinates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x.GetHashCode() * 397) ^ _y.GetHashCode();
            }
        }

        public static bool operator ==(ChromaticityCoordinates left, ChromaticityCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChromaticityCoordinates left, ChromaticityCoordinates right)
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