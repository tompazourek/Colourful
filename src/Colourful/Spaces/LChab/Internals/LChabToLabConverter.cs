using static Colourful.CylindricalFormulas;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class LChabToLabConverter : IColorConverter<LChabColor, LabColor>
    {
        /// <inheritdoc />
        public LabColor Convert(in LChabColor sourceColor)
        {
            var targetVector = ConvertFromLCh(sourceColor.Vector);
            var targetColor = new LabColor(in targetVector);
            return targetColor;
        }
    }
}