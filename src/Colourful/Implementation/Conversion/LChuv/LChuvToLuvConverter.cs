using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LChuvColor" /> to <see cref="LuvColor" />.
    /// </summary>
    public sealed class LChuvToLuvConverter : IColorConversion<LChuvColor, LuvColor>, IEquatable<LChuvToLuvConverter>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LChuvToLuvConverter Default = new LChuvToLuvConverter();

        /// <summary>
        /// Converts from <see cref="LChuvColor" /> to <see cref="LuvColor" />.
        /// </summary>
        public LuvColor Convert(in LChuvColor input)
        {
            double L = input.L, C = input.C, hDegrees = input.h;
            var hRadians = Angle.DegreeToRadian(in hDegrees);

            var u = C * Math.Cos(hRadians);
            var v = C * Math.Sin(hRadians);

            var output = new LuvColor(in L, in u, in v, input.WhitePoint);
            return output;
        }
        
        #region Equality
        
        /// <inheritdoc />
        public bool Equals(LChuvToLuvConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LChuvToLuvConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LChuvToLuvConverter left, LChuvToLuvConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LChuvToLuvConverter left, LChuvToLuvConverter right) => !Equals(left, right);

        #endregion
    }
}