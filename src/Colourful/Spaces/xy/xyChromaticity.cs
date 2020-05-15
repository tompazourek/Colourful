﻿using System;
using System.Globalization;

namespace Colourful
{
    /// <summary>
    /// Coordinates of CIE xy chromaticity space
    /// </summary>
    public readonly struct xyChromaticity : IColorVector, IEquatable<xyChromaticity>
    {
        /// <param name="x">Chromaticity coordinate x (usually from 0 to 1)</param>
        /// <param name="y">Chromaticity coordinate y (usually from 0 to 1)</param>
        public xyChromaticity(in double x, in double y)
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
        public readonly double x;

        /// <summary>
        /// Chromaticity coordinate y
        /// </summary>
        /// <remarks>
        /// Ranges usually from 0 to 1.
        /// </remarks>
        public readonly double y;

        /// <summary>
        /// <see cref="IColorVector" />
        /// </summary>
        public double[] Vector => new[] { x, y };

        #region Equality

        /// <inheritdoc />
        public bool Equals(xyChromaticity other) => x.Equals(other.x) && y.Equals(other.y);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is xyChromaticity coordinates && Equals(coordinates);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        /// <inheritdoc cref="object" />
        public static bool operator ==(xyChromaticity left, xyChromaticity right) => left.Equals(right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(xyChromaticity left, xyChromaticity right) => !left.Equals(right);

        #endregion

        #region Overrides

        /// <inheritdoc />
        public override string ToString() => string.Format(CultureInfo.InvariantCulture, "xy [x={0:0.##}, y={1:0.##}]", x, y);

        #endregion
    }
}