using Xunit;

namespace Colourful.Tests
{
    public class LinearRGBColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new LinearRGBColor(r: .1, g: .205, b: .45445);
            var second = new LinearRGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LinearRGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal("LinearRGB [R=0.1, G=0.21, B=0.45]", color.ToString());
        }
    }
}