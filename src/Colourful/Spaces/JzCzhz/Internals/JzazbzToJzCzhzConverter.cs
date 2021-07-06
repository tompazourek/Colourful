using static Colourful.CylindricalFormulas;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class JzazbzToJzCzhzConverter : IColorConverter<JzazbzColor, JzCzhzColor>
    {
        /// <inheritdoc />
        public JzCzhzColor Convert(in JzazbzColor sourceColor)
        {
            var targetVector = ConvertToLCh(sourceColor.Vector);
            var targetColor = new JzCzhzColor(in targetVector);
            return targetColor;
        }
    }
}
