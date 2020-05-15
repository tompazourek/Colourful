using static Colourful.LChFormulas;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class LuvToLChuvConverter : IColorConverter<LuvColor, LChuvColor>
    {
        /// <inheritdoc />
        public LChuvColor Convert(in LuvColor sourceColor)
        {
            var targetVector = ConvertToLCh(sourceColor.Vector);
            var targetColor = new LChuvColor(in targetVector);
            return targetColor;
        }
    }
}