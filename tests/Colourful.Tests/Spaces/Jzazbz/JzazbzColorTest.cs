using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class JzazbzColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            var second = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new JzazbzColor(jz: 0.7, az: -0.3, bz: 0.01);
            var second = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            var vector = new[] { 0.6, -0.3, 0.01 };
            var second = new JzazbzColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new JzazbzColor(jz: 0.6, az: -0.3, bz: 0.01);
            Assert.Equal("Jzazbz [Jz=0.6, az=-0.3, bz=0.01]", color.ToString());
        }

        [Fact]
        public void Dctor()
        {
            const double jz1 = 0.6;
            const double az1 = -0.3;
            const double bz1 = 0.01;
            var (jz2, az2, bz2) = new JzazbzColor(jz1, az1, bz1);
            Assert.Equal(jz1, jz2);
            Assert.Equal(az1, az2);
            Assert.Equal(bz1, bz2);
        }
    }
}
