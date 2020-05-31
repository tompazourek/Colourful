using Xunit;

namespace Colourful.Tests
{
    public class LChabColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new LChabColor(l: 10, c: 20.5, h: 45.445);
            var second = new LChabColor(l: 10, c: 20.5, h: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LChabColor(l: 10, c: 20.5, h: 45.445);
            Assert.Equal("LChab [L=10, C=20.5, h=45.45]", color.ToString());
        }
    }
}