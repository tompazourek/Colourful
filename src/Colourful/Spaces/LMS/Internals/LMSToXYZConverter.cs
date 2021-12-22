using static Colourful.Internals.MatrixUtils;

namespace Colourful.Internals;

/// <inheritdoc />
public class LMSToXYZConverter : IColorConverter<LMSColor, XYZColor>
{
    private readonly double[,] _transformationMatrixInverse;

    /// <param name="transformationMatrix">Definition of the cone response domain (see <see cref="LMSTransformationMatrix" />).</param>
    public LMSToXYZConverter(in double[,] transformationMatrix) => _transformationMatrixInverse = Inverse(transformationMatrix);

    /// <inheritdoc />
    public XYZColor Convert(in LMSColor sourceColor)
    {
        var sourceVector = sourceColor.Vector;
        var targetVector = MultiplyBy(in _transformationMatrixInverse, in sourceVector);
        var targetColor = new XYZColor(in targetVector);
        return targetColor;
    }
}
