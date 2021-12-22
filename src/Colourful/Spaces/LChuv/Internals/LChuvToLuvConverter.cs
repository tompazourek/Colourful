using static Colourful.CylindricalFormulas;

namespace Colourful.Internals;

/// <inheritdoc />
public class LChuvToLuvConverter : IColorConverter<LChuvColor, LuvColor>
{
    /// <inheritdoc />
    public LuvColor Convert(in LChuvColor sourceColor)
    {
        var targetVector = ConvertFromLCh(sourceColor.Vector);
        var targetColor = new LuvColor(in targetVector);
        return targetColor;
    }
}
