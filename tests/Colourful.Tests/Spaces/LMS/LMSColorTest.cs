using Colourful.Tests.Assertions;
using Xunit;

namespace Colourful.Tests
{
    public class LMSColorTest
    {
        [Fact]
        public void Equals_Same()
        {
            var first = new LMSColor(l: .1, m: .205, s: .45445);
            var second = new LMSColor(l: .1, m: .205, s: .45445);
            CustomAssert.EqualsWithHashCode(first, second);
        }

        [Fact]
        public void Equals_Different()
        {
            var first = new LMSColor(l: .2, m: .205, s: .45445);
            var second = new LMSColor(l: .1, m: .205, s: .45445);
            CustomAssert.NotEqualsWithHashCode(first, second);
        }

        [Fact]
        public void VectorCtor()
        {
            var first = new LMSColor(l: .2, m: .205, s: .45445);
            var vector = new[] { .2, .205, .45445 };
            var second = new LMSColor(vector);
            CustomAssert.EqualsWithHashCode(first, second);
            Assert.Equal(vector, second.Vector);
        }

        [Fact]
        public void ToString_Simple()
        {
            var color = new LMSColor(l: .1, m: .205, s: .45445);
            Assert.Equal("LMS [L=0.1, M=0.21, S=0.45]", color.ToString());
        }

        [Fact]
        public void Dctor()
        {
            const double l1 = .1;
            const double m1 = .205;
            const double s1 = .45445;
            var (l2, m2, s2) = new LMSColor(l1, m1, s1);
            Assert.Equal(l1, l2);
            Assert.Equal(m1, m2);
            Assert.Equal(s1, s2);
        }
    }
}
