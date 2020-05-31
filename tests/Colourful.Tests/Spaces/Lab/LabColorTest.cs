using Xunit;

namespace Colourful.Tests
{
    public class LabColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new LabColor(l: 10, a: 20.5, b: 45.445);
            var second = new LabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LabColor(l: 10, a: 20.5, b: 45.445);
            Assert.Equal("Lab [L=10, a=20.5, b=45.45]", color.ToString());
        }
    }
}