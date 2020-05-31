using Xunit;

namespace Colourful.Tests
{
    public class xyChromaticityTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new xyChromaticity(x: .1, y: .205);
            var second = new xyChromaticity(x: .1, y: .205);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new xyChromaticity(x: .1, y: .205);
            Assert.Equal("xy [x=0.1, y=0.21]", color.ToString());
        }
    }
}