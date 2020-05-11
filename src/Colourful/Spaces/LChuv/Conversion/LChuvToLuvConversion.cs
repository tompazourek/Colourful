namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LChuvToLuvConversion : IColorConversion<LChuvColor, LuvColor>
    {
        /// <inheritdoc />
        public LuvColor Convert(in LChuvColor sourceColor)
        {
            var targetVector = LChFormulas.ConvertFromLCh(sourceColor.Vector);
            var targetColor = new LuvColor(in targetVector);
            return targetColor;
        }
    }
}