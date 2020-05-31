using Xunit;

namespace Colourful.Tests
{
    public class JzazbzColorTest
    {
        [Fact]
        public void Equals_Simple()
        {
            var first = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            var second = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            Assert.Equal(first, (object)second);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            Assert.Equal("Jzazbz [Jz=0.6, az=-0.3, bz=0.01]", color.ToString());
        }
    }
}