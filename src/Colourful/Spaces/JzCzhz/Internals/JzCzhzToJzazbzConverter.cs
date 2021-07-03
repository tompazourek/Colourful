using static Colourful.CylindricalFormulas;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class JzCzhzToJzazbzConverter : IColorConverter<JzCzhzColor, JzazbzColor>
    {
        /// <inheritdoc />
        public JzazbzColor Convert(in JzCzhzColor sourceColor)
        {
            var targetVector = ConvertFromLCh(sourceColor.Vector);
            var targetColor = new JzazbzColor(in targetVector);
            return targetColor;
        }
    }
}
