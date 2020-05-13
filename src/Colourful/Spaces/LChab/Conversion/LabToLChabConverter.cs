using Colourful.Utils;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LabToLChabConverter : IColorConverter<LabColor, LChabColor>
    {
        /// <inheritdoc />
        public LChabColor Convert(in LabColor sourceColor)
        {
            var targetVector = LChFormulas.ConvertToLCh(sourceColor.Vector);
            var targetColor = new LChabColor(in targetVector);
            return targetColor;
        }
    }
}