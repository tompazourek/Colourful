using Xunit;

namespace Colourful.Tests
{
    public class xyYColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new xyYColor(x: .1, y: .205, Y: .45445);
            var second = new xyYColor(x: .1, y: .205, Y: .45445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new xyYColor(x: .1, y: .205, Y: .45445);
            Assert.Equal("xyY [x=0.1, y=0.21, Y=0.45]", color.ToString());
        }
    }
}