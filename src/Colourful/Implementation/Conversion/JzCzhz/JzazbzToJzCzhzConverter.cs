using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="JzazbzColor" /> to <see cref="JzCzhzColor" />.
    /// </summary>
    public sealed class JzazbzToJzCzhzConverter : IColorConversion<JzazbzColor, JzCzhzColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly JzazbzToJzCzhzConverter Default = new JzazbzToJzCzhzConverter();

        /// <summary>
        /// Converts from <see cref="JzazbzColor" /> to <see cref="JzCzhzColor" />.
        /// </summary>
        public JzCzhzColor Convert(in JzazbzColor input)
        {
            double Jz = input.Jz, a = input.az, b = input.bz;
            var Cz = Math.Sqrt(a * a + b * b);
            var hRadians = Math.Atan2(b, a);
            var hzDegrees = Angle.NormalizeDegree(Angle.RadianToDegree(hRadians));

            var output = new JzCzhzColor(in Jz, in Cz, in hzDegrees);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is JzazbzToJzCzhzConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(JzazbzToJzCzhzConverter left, JzazbzToJzCzhzConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(JzazbzToJzCzhzConverter left, JzazbzToJzCzhzConverter right) => !Equals(left, right);

        #endregion
    }
}