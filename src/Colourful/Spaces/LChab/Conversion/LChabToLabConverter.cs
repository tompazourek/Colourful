using Colourful.Utils;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LChabToLabConverter : IColorConverter<LChabColor, LabColor>
    {
        /// <inheritdoc />
        public LabColor Convert(in LChabColor sourceColor)
        {
            var targetVector = LChFormulas.ConvertFromLCh(sourceColor.Vector);
            var targetColor = new LabColor(in targetVector);
            return targetColor;
        }
    }
}