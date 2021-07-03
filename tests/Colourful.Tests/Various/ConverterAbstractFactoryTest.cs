using System.Linq;
using Colourful.Internals;
using Xunit;

namespace Colourful.Tests
{
    public class ConverterAbstractFactoryTest
    {
        [Fact]
        public void NoConversionExists_Throws()
        {
            // arrange
            var conversionStrategies = ConversionStrategies.GetDefault().Where(x => !(x is LabConversionStrategy));
            var conversionAbstractFactory = new ConverterAbstractFactory(conversionStrategies);

            // action & assert
            var ex = Assert.Throws<InvalidConversionException>(() => { conversionAbstractFactory.CreateConverter<LabColor, XYZColor>(ConversionMetadata.Empty, ConversionMetadata.Empty); });

            Assert.Equal(typeof(LabColor), ex.SourceType);
            Assert.Equal(typeof(XYZColor), ex.TargetType);
        }
    }
}
