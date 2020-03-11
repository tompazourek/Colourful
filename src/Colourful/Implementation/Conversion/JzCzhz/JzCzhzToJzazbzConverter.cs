using System;

namespace Colourful.Implementation.Conversion
{
    /// <summary>
    /// Converts from <see cref="JzCzhzColor" /> to <see cref="JzazbzColor" />.
    /// </summary>
    public sealed class JzCzhzToJzazbzConverter : IColorConversion<JzCzhzColor, JzazbzColor>
    {
        /// <summary>
        /// Default singleton instance of the converter.
        /// </summary>
        public static readonly JzCzhzToJzazbzConverter Default = new JzCzhzToJzazbzConverter();

        /// <summary>
        /// Converts from <see cref="JzCzhzColor" /> to <see cref="JzazbzColor" />.
        /// </summary>
        public JzazbzColor Convert(in JzCzhzColor input)
        {
            double Jz = input.Jz, Cz = input.Cz, hzDegrees = input.hz;
            var hzRadians = Angle.DegreeToRadian(hzDegrees);

            var az = Cz * Math.Cos(hzRadians);
            var bz = Cz * Math.Sin(hzRadians);

            var output = new JzazbzColor(Jz, az, bz);
            return output;
        }

        #region Overrides

        /// <inheritdoc cref="object" />
        public override bool Equals(object obj) => obj is JzCzhzToJzazbzConverter;

        /// <inheritdoc cref="object" />
        public override int GetHashCode() => 1;

        /// <inheritdoc cref="object" />
        public static bool operator ==(JzCzhzToJzazbzConverter left, JzCzhzToJzazbzConverter right) => Equals(left, right);

        /// <inheritdoc cref="object" />
        public static bool operator !=(JzCzhzToJzazbzConverter left, JzCzhzToJzazbzConverter right) => !Equals(left, right);

        #endregion
    }
}