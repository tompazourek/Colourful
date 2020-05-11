using Colourful.Companding;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class LinearRGBToRGBConversion : IColorConversion<LinearRGBColor, RGBColor>
    {
        private readonly ICompanding _targetCompanding;

        /// <param name="targetCompanding">Companding function of the target RGB working space</param>
        public LinearRGBToRGBConversion(ICompanding targetCompanding)
        {
            _targetCompanding = targetCompanding;
        }

        /// <inheritdoc />
        public RGBColor Convert(in LinearRGBColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            double[] targetVector =
            {
                _targetCompanding.ConvertToNonLinear(sourceVector[0]),
                _targetCompanding.ConvertToNonLinear(sourceVector[1]),
                _targetCompanding.ConvertToNonLinear(sourceVector[2]),
            };
            var targetColor = new RGBColor(in targetVector);
            return targetColor;
        }
    }
}