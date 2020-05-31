using Xunit;

namespace Colourful.Tests
{
    public class HunterLabColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            var second = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new HunterLabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal("HunterLab [L=10, a=20.5, b=45.45]", color.ToString());
        }
    }
}