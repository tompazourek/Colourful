using Xunit;

namespace Colourful.Tests
{
    public class JzCzhzColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new JzCzhzColor(jz: 0.6, cz: 0.3, hz: 150);
            var second = new JzCzhzColor(jz: 0.6, cz: 0.3, hz: 150);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new JzCzhzColor(jz: 0.6, cz: 0.3, hz: 150);
            Assert.Equal("JzCzhz [Jz=0.6, Cz=0.3, hz=150]", color.ToString());
        }
    }
}