using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
    /// </summary>
    public sealed class LuvToLChuvConverter : IColorConversion<LuvColor, LChuvColor>, IEquatable<LuvToLChuvConverter>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly LuvToLChuvConverter Default = new LuvToLChuvConverter();

        /// <summary>
        /// Converts from <see cref="LuvColor" /> to <see cref="LChuvColor" />.
        /// </summary>
        public LChuvColor Convert(in LuvColor input)
        {
            double L = input.L, u = input.u, v = input.v;
            var C = Math.Sqrt(u * u + v * v);
            var hRadians = Math.Atan2(v, u);
            var hDegrees = Angle.NormalizeDegree(Angle.RadianToDegree(in hRadians));

            var output = new LChuvColor(in L, in C, in hDegrees, input.WhitePoint);
            return output;
        }
        
        #region Equality
        
        /// <inheritdoc />
        public bool Equals(LuvToLChuvConverter other)
        {
            if (other == null)
                return false;

            return true;
        }

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is LuvToLChuvConverter;

        /// <inheritdoc />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(LuvToLChuvConverter left, LuvToLChuvConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(LuvToLChuvConverter left, LuvToLChuvConverter right) => !Equals(left, right);

        #endregion
    }
}