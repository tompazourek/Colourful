using Xunit;

namespace Colourful.Tests
{
    public class LuvColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new LuvColor(l: 10, u: 20.5, v: 45.445);
            var second = new LuvColor(l: 10, u: 20.5, v: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LuvColor(l: 10, u: 20.5, v: 45.445);
            Assert.Equal("Luv [L=10, u=20.5, v=45.45]", color.ToString());
        }
    }
}