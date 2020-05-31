using Xunit;

namespace Colourful.Tests
{
    public class RGBColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new RGBColor(r: .1, g: .205, b: .45445);
            var second = new RGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new RGBColor(r: .1, g: .205, b: .45445);
            Assert.Equal("RGB [R=0.1, G=0.21, B=0.45]", color.ToString());
        }
    }
}