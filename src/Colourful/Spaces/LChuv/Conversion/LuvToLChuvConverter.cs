using Colourful.Utils;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LuvToLChuvConverter : IColorConverter<LuvColor, LChuvColor>
    {
        /// <inheritdoc />
        public LChuvColor Convert(in LuvColor sourceColor)
        {
            var targetVector = LChFormulas.ConvertToLCh(sourceColor.Vector);
            var targetColor = new LChuvColor(in targetVector);
            return targetColor;
        }
    }
}