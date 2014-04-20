using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.Implementation.RGB;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests equals on RGB working space.
    /// </summary>
    [TestFixture]
    public class RGBWorkingSpaceEqualsTest
    {
        private class AdobeRGB1998Duplicate : IRGBWorkingSpace
        {
            public ICompanding Companding
            {
                get { return new GammaCompanding(2.2); }
            }

            public XYZColor WhitePoint
            {
                get { return Illuminants.D65; }
            }

            public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
            {
                get { return new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.2100, 0.7100), new ChromaticityCoordinates(0.1500, 0.0600)); }
            }
        }

        [Test]
        public void DifferentWorkingSpace_IsNotEqual()
        {
            // arrange
            IRGBWorkingSpace x = RGBWorkingSpaces.sRGB;
            IRGBWorkingSpace y = RGBWorkingSpaces.AdobeRGB1998;

            // act
            bool equals = x.Equals(y);

            // assert
            Assert.IsFalse(equals);
        }

        [Test]
        public void DifferentWorkingSpace_SameSpecifiers_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = new AdobeRGB1998Duplicate();
            IRGBWorkingSpace y = RGBWorkingSpaces.AdobeRGB1998;

            // act
            bool equals = y.Equals(x);

            // assert
            Assert.IsTrue(equals);
        }

        [Test]
        public void SameReference_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = RGBWorkingSpaces.sRGB;
            IRGBWorkingSpace y = x;

            // act
            bool equals = x.Equals(y);

            // assert
            Assert.IsTrue(equals);
        }

        [Test]
        public void SameWorkingSpace_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = RGBWorkingSpaces.sRGB;
            IRGBWorkingSpace y = RGBWorkingSpaces.sRGB;

            // act
            bool equals = x.Equals(y);

            // assert
            Assert.IsTrue(equals);
        }
    }
}