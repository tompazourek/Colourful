using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Colors
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public struct ChromaticityCoordinates
    {
        private readonly double _x;
        private readonly double _y;

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public ChromaticityCoordinates(double x, double y)
        {
            _x = x;
            _y = y;
        }

        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "x"), SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x")]
        public double x
        {
            get { return _x; }
        }

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
    }
}