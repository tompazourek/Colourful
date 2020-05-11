using Colourful.Companding;

namespace Colourful.Conversion
{
    /// <inheritdoc />
    public class RGBToLinearRGBConversion : IColorConversion<RGBColor, LinearRGBColor>
    {
        private readonly ICompanding _sourceCompanding;

        /// <param name="sourceCompanding">Companding function of the source RGB working space</param>
        public RGBToLinearRGBConversion(ICompanding sourceCompanding)
        {
            _sourceCompanding = sourceCompanding;
        }

        /// <inheritdoc />
        public LinearRGBColor Convert(in RGBColor sourceColor)
        {
            var sourceVector = sourceColor.Vector;
            double[] targetVector = 
            {
                _sourceCompanding.ConvertToLinear(sourceVector[0]),
                _sourceCompanding.ConvertToLinear(sourceVector[1]),
                _sourceCompanding.ConvertToLinear(sourceVector[2]),
            };
            var targetColor = new LinearRGBColor(in targetVector);
            return targetColor;
        }
    }
}