using Colourful.Implementation.RGB;
using Xunit;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests equals on RGB working space.
    /// </summary>
    public class RGBWorkingSpaceEqualsTest
    {
        private class AdobeRGB1998Duplicate : IRGBWorkingSpace
        {
            public ICompanding Companding => new GammaCompanding(2.2);

            public XYZColor WhitePoint => Illuminants.D65;

            public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates => new RGBPrimariesChromaticityCoordinates(new xyChromaticityCoordinates(0.6400, 0.3300), new xyChromaticityCoordinates(0.2100, 0.7100), new xyChromaticityCoordinates(0.1500, 0.0600));
        }

        [Fact]
        public void DifferentWorkingSpace_IsNotEqual()
        {
            // arrange
            var x = RGBWorkingSpaces.sRGB;
            var y = RGBWorkingSpaces.AdobeRGB1998;

            // act
            var equals = x.Equals(y);

            // assert
            Assert.False(equals);
        }

        [Fact]
        public void DifferentWorkingSpace_SameSpecifiers_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = new AdobeRGB1998Duplicate();
            var y = RGBWorkingSpaces.AdobeRGB1998;

            // act
            var equals = y.Equals(x);

            // assert
            Assert.True(equals);
        }

        [Fact]
        public void SameReference_IsEqual()
        {
            // arrange
            var x = RGBWorkingSpaces.sRGB;
            var y = x;

            // act
            var equals = x.Equals(y);

            // assert
            Assert.True(equals);
        }

        [Fact]
        public void SameWorkingSpace_IsEqual()
        {
            // arrange
            var x = RGBWorkingSpaces.sRGB;
            var y = RGBWorkingSpaces.sRGB;

            // act
            var equals = x.Equals(y);

            // assert
            Assert.True(equals);
        }
    }
}