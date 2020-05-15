using static Colourful.CylindricalFormulas;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class LabToLChabConverter : IColorConverter<LabColor, LChabColor>
    {
        /// <inheritdoc />
        public LChabColor Convert(in LabColor sourceColor)
        {
            var targetVector = ConvertToLCh(sourceColor.Vector);
            var targetColor = new LChabColor(in targetVector);
            return targetColor;
        }
    }
}