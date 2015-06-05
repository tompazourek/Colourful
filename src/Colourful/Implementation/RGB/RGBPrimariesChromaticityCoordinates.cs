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

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Chromaticity coordinates of RGB primaries.
    /// One of the specifiers of <see cref="IRGBWorkingSpace"/>.
    /// </summary>
    public struct RGBPrimariesChromaticityCoordinates
    {
        private readonly xyChromaticityCoordinates _b;
        private readonly xyChromaticityCoordinates _g;
        private readonly xyChromaticityCoordinates _r;

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "g"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r")]
        public RGBPrimariesChromaticityCoordinates(xyChromaticityCoordinates r, xyChromaticityCoordinates g, xyChromaticityCoordinates b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        /// <summary>
        /// Red
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        public xyChromaticityCoordinates R
        {
            get { return _r; }
        }

        /// <summary>
        /// Green
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G")]
        public xyChromaticityCoordinates G
        {
            get { return _g; }
        }

        /// <summary>
        /// Blue
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        public xyChromaticityCoordinates B
        {
            get { return _b; }
        }

        public bool Equals(RGBPrimariesChromaticityCoordinates other)
        {
            return _r.Equals(other._r) && _g.Equals(other._g) && _b.Equals(other._b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is RGBPrimariesChromaticityCoordinates && Equals((RGBPrimariesChromaticityCoordinates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _r.GetHashCode();
                hashCode = (hashCode * 397) ^ _g.GetHashCode();
                hashCode = (hashCode * 397) ^ _b.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RGBPrimariesChromaticityCoordinates left, RGBPrimariesChromaticityCoordinates right)
        {
            return !left.Equals(right);
        }
    }
}