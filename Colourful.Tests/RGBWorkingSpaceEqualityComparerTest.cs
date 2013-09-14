using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.RGBWorkingSpaces;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests <see cref="RGBWorkingSpaceEqualityComparer"/>.
    /// </summary>
    [TestFixture]
    public class RGBWorkingSpaceEqualityComparerTest
    {
        [Test]
        public void SameReference_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = new sRGBWorkingSpace();
            IRGBWorkingSpace y = x;

            // act
            bool equals = RGBWorkingSpaceEqualityComparer.Default.Equals(x, y);

            // assert
            Assert.IsTrue(equals);
        }

        [Test]
        public void SameWorkingSpace_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = new sRGBWorkingSpace();
            IRGBWorkingSpace y = new sRGBWorkingSpace();

            // act
            bool equals = RGBWorkingSpaceEqualityComparer.Default.Equals(x, y);

            // assert
            Assert.IsTrue(equals);
        }

        [Test]
        public void DifferentWorkingSpace_IsNotEqual()
        {
            // arrange
            IRGBWorkingSpace x = new sRGBWorkingSpace();
            IRGBWorkingSpace y = new AdobeRGB1998();

            // act
            bool equals = RGBWorkingSpaceEqualityComparer.Default.Equals(x, y);

            // assert
            Assert.IsFalse(equals);
        }

        private class AdobeRGB1998Duplicate : IRGBWorkingSpace
        {
            public ICompanding Companding
            {
                get { return new GammaCompanding(2.2); }
            }

            public XYZColorBase ReferenceWhite
            {
                get { return Illuminants.D65; }
            }

            public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates
            {
                get { return new RGBPrimariesChromaticityCoordinates(new ChromaticityCoordinates(0.6400, 0.3300), new ChromaticityCoordinates(0.2100, 0.7100), new ChromaticityCoordinates(0.1500, 0.0600)); }
            }
        }

        [Test]
        public void DifferentWorkingSpace_SameSpecifiers_IsEqual()
        {
            // arrange
            IRGBWorkingSpace x = new AdobeRGB1998Duplicate();
            IRGBWorkingSpace y = new AdobeRGB1998();

            // act
            bool equals = RGBWorkingSpaceEqualityComparer.Default.Equals(x, y);

            // assert
            Assert.IsTrue(equals);
        }
    }
}
