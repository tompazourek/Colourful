using Xunit;

namespace Colourful.Tests
{
    public class LMSColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new LMSColor(l: .1, m: .205, s: .45445);
            var second = new LMSColor(l: .1, m: .205, s: .45445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LMSColor(l: .1, m: .205, s: .45445);
            Assert.Equal("LMS [L=0.1, M=0.21, S=0.45]", color.ToString());
        }
    }
}