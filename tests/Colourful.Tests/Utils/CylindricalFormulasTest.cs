using Xunit;

namespace Colourful.Tests
{
    public class CylindricalFormulasTest
    {
        [Fact]
        public void GetSaturation_Zeroes()
        {
            var result = CylindricalFormulas.GetSaturation(L: 0, C: 0);
            Assert.Equal(expected: 0, result);
        }
    }
}
