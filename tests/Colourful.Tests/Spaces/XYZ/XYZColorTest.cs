using Xunit;

namespace Colourful.Tests
{
    public class XYZColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new XYZColor(x: .1, y: .205, z: .45445);
            var second = new XYZColor(x: .1, y: .205, z: .45445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new XYZColor(x: .1, y: .205, z: .45445);
            Assert.Equal("XYZ [X=0.1, Y=0.21, Z=0.45]", color.ToString());
        }
    }
}