using Colourful.Strategy;
using Colourful.Utils;
using Xunit;
using static Colourful.Strategy.ConversionMetadataUtils;

namespace Colourful.Tests
{
    public class StrategyBasedConverterFactoryTest
    {
        [Fact]
        public void LChabToHunterLab_DifferentWhitePoints()
        {
            var converterFactory = new StrategyBasedConverterFactory(ConversionStrategies.GetDefault());
            var converter = converterFactory.CreateConverter<LChabColor, HunterLabColor>(new ConversionMetadata(CreateWhitePoint(Illuminants.D50)), new ConversionMetadata(CreateWhitePoint(Illuminants.D65)));
        }
    }
}