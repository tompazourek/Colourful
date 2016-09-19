#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
// 
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Colourful.Difference;
using Colourful.Implementation;
using NUnit.Framework;

namespace Colourful.Tests
{
    /// <summary>
    /// Tests color difference formulas
    /// </summary>
    [TestFixture]
    public class ColorDifferenceFormulasTest
    {
        private static readonly IComparer<double> DoubleComparerLabPrecision = new DoublePrecisionComparer(4);
        private static readonly IComparer<double> DoubleComparerLabRounding = new DoubleRoundingComparer(4);

        /// <summary>
        /// Tests <see cref="Difference.CIE76ColorDifference"/>
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0, 0, 0, 100)]
        [TestCase(100, -50, 50, 20, 10, -20, 122.06555)]
        [TestCase(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 196.27915)]
        public void CIE76ColorDifference(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            double deltaE = new CIE76ColorDifference().ComputeDifference(x, y);

            // assert
            Assert.That(deltaE, Is.EqualTo(expectedDeltaE).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests <see cref="CIE94ColorDifference"/> for <see cref="CIE94ColorDifferenceApplication.GraphicArts"/>
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0, 0, 0, 100)]
        [TestCase(100, -50, 50, 20, 10, -20, 89.358114)]
        [TestCase(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 51.361909)]
        public void CIE94ColorDifference_GraphicArts(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            double deltaE = new CIE94ColorDifference(CIE94ColorDifferenceApplication.GraphicArts).ComputeDifference(x, y);

            // assert
            Assert.That(deltaE, Is.EqualTo(expectedDeltaE).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests <see cref="CIE94ColorDifference"/> for <see cref="CIE94ColorDifferenceApplication.Textiles"/>
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0, 0, 0, 50)]
        [TestCase(100, -50, 50, 20, 10, -20, 57.247221)]
        [TestCase(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 37.179939)]
        public void CIE94ColorDifference_Textiles(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            double deltaE = new CIE94ColorDifference(CIE94ColorDifferenceApplication.Textiles).ComputeDifference(x, y);

            // assert
            Assert.That(deltaE, Is.EqualTo(expectedDeltaE).Using(DoubleComparerLabPrecision));
        }

        /// <summary>
        /// Tests <see cref="Difference.CIEDE2000ColorDifference"/>
        /// </summary>
        /// <remarks>
        /// Test data from:
        /// Sharma, Gaurav; Wencheng Wu, Edul N. Dalal (2005). "The CIEDE2000 color-difference formula: Implementation notes, supplementary test data, and mathematical observations" (http://www.ece.rochester.edu/~gsharma/ciede2000/ciede2000noteCRNA.pdf)
        /// </remarks>
        [Test]
        [TestCase(50.0000, 2.6772, -79.7751, 50.0000, 0.0000, -82.7485, 2.0425)]
        [TestCase(50.0000, 3.1571, -77.2803, 50.0000, 0.0000, -82.7485, 2.8615)]
        [TestCase(50.0000, 2.8361, -74.0200, 50.0000, 0.0000, -82.7485, 3.4412)]
        [TestCase(50.0000, -1.3802, -84.2814, 50.0000, 0.0000, -82.7485, 1.0000)]
        [TestCase(50.0000, -1.1848, -84.8006, 50.0000, 0.0000, -82.7485, 1.0000)]
        [TestCase(50.0000, -0.9009, -85.5211, 50.0000, 0.0000, -82.7485, 1.0000)]
        [TestCase(50.0000, 0.0000, 0.0000, 50.0000, -1.0000, 2.0000, 2.3669)]
        [TestCase(50.0000, -1.0000, 2.0000, 50.0000, 0.0000, 0.0000, 2.3669)]
        [TestCase(50.0000, 2.4900, -0.0010, 50.0000, -2.4900, 0.0009, 7.1792)]
        [TestCase(50.0000, 2.4900, -0.0010, 50.0000, -2.4900, 0.0010, 7.1792)]
        [TestCase(50.0000, 2.4900, -0.0010, 50.0000, -2.4900, 0.0011, 7.2195)]
        [TestCase(50.0000, 2.4900, -0.0010, 50.0000, -2.4900, 0.0012, 7.2195)]
        [TestCase(50.0000, -0.0010, 2.4900, 50.0000, 0.0009, -2.4900, 4.8045)]
        [TestCase(50.0000, -0.0010, 2.4900, 50.0000, 0.0010, -2.4900, 4.8045)]
        [TestCase(50.0000, -0.0010, 2.4900, 50.0000, 0.0011, -2.4900, 4.7461)]
        [TestCase(50.0000, 2.5000, 0.0000, 50.0000, 0.0000, -2.5000, 4.3065)]
        [TestCase(50.0000, 2.5000, 0.0000, 73.0000, 25.0000, -18.0000, 27.1492)]
        [TestCase(50.0000, 2.5000, 0.0000, 61.0000, -5.0000, 29.0000, 22.8977)]
        [TestCase(50.0000, 2.5000, 0.0000, 56.0000, -27.0000, -3.0000, 31.9030)]
        [TestCase(50.0000, 2.5000, 0.0000, 58.0000, 24.0000, 15.0000, 19.4535)]
        [TestCase(50.0000, 2.5000, 0.0000, 50.0000, 3.1736, 0.5854, 1.0000)]
        [TestCase(50.0000, 2.5000, 0.0000, 50.0000, 3.2972, 0.0000, 1.0000)]
        [TestCase(50.0000, 2.5000, 0.0000, 50.0000, 1.8634, 0.5757, 1.0000)]
        [TestCase(50.0000, 2.5000, 0.0000, 50.0000, 3.2592, 0.3350, 1.0000)]
        [TestCase(60.2574, -34.0099, 36.2677, 60.4626, -34.1751, 39.4387, 1.2644)]
        [TestCase(63.0109, -31.0961, -5.8663, 62.8187, -29.7946, -4.0864, 1.2630)]
        [TestCase(61.2901, 3.7196, -5.3901, 61.4292, 2.2480, -4.9620, 1.8731)]
        [TestCase(35.0831, -44.1164, 3.7933, 35.0232, -40.0716, 1.5901, 1.8645)]
        [TestCase(22.7233, 20.0904, -46.6940, 23.0331, 14.9730, -42.5619, 2.0373)]
        [TestCase(36.4612, 47.8580, 18.3852, 36.2715, 50.5065, 21.2231, 1.4146)]
        [TestCase(90.8027, -2.0831, 1.4410, 91.1528, -1.6435, 0.0447, 1.4441)]
        [TestCase(90.9257, -0.5406, -0.9208, 88.6381, -0.8985, -0.7239, 1.5381)]
        [TestCase(6.7747, -0.2908, -2.4247, 5.8714, -0.0985, -2.2286, 0.6377)]
        [TestCase(2.0776, 0.0795, -1.1350, 0.9033, -0.0636, -0.5514, 0.9082)]
        public void CIEDE2000ColorDifference(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            double deltaE = new CIEDE2000ColorDifference().ComputeDifference(x, y);

            // assert
            Assert.That(deltaE, Is.EqualTo(expectedDeltaE).Using(DoubleComparerLabRounding));
        }

        /// <summary>
        /// Tests <see cref="CMCColorDifference"/>
        /// </summary>
        /// <remarks>
        /// Test data generated using:
        /// http://www.brucelindbloom.com/index.html?ColorDifferenceCalc.html
        /// </remarks>
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(100, 0, 0, 0, 0, 0, 67.480171, 33.740085)]
        [TestCase(100, -50, 50, 20, 10, -20, 66.320207, 47.038863)]
        [TestCase(10.3454, 3.2151, -189.1230, 51.7781, -22.5151, 1.0001, 98.577755, 69.187455)]
        [TestCase(41.41, 2.64, 4.15, 0, 0, 0, 42.570316, 21.769476)]
        public void CMCColorDifference(double l1, double a1, double b1, double l2, double a2, double b2, double expectedDeltaE_imperceptibility, double expectedDeltaE_acceptability)
        {
            // arrange
            var x = new LabColor(l1, a1, b1);
            var y = new LabColor(l2, a2, b2);

            // act
            double deltaE_imperceptibility = new CMCColorDifference(CMCColorDifferenceThreshold.Imperceptibility).ComputeDifference(x, y);
            double deltaE_acceptability = new CMCColorDifference(CMCColorDifferenceThreshold.Acceptability).ComputeDifference(x, y);


            // assert
            Assert.That(deltaE_imperceptibility, Is.EqualTo(expectedDeltaE_imperceptibility).Using(DoubleComparerLabPrecision));
            Assert.That(deltaE_acceptability, Is.EqualTo(expectedDeltaE_acceptability).Using(DoubleComparerLabPrecision));
        }
    }
}