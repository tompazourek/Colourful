using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Colourful.Tests
{
    public class CCTTest
    {
        /// <summary>
        /// Data from: http://en.wikipedia.org/wiki/Standard_illuminant#White_points_of_standard_illuminants
        /// </summary>
        public static readonly IEnumerable<object[]> CCTData_Wikipedia = new []
        {
            new object[] { 0.44757, 0.40745, 2856 }, // (A) Incandescent / Tungsten
            new object[] { 0.34842, 0.35161, 4874 }, // (B) Direct sunlight at noon
            new object[] { 0.31006, 0.31616, 6774 }, // (C)  Average / North sky Daylight
            new object[] { 0.34567, 0.35850, 5003 }, // (D50) Horizon Light. ICC profile PCS
            new object[] { 0.33242, 0.34743, 5503 }, // (D55) Mid-morning / Mid-afternoon Daylight
            new object[] { 0.31271, 0.32902, 6504 }, // (D65) Noon Daylight: Television, sRGB color space
            new object[] { 0.29902, 0.31485, 7504 }, // (D75) North sky Daylight
            new object[] { 1 / 3d, 1 / 3d, 5454 }, // (E) Equal energy
            new object[] { 0.31310, 0.33727, 6430 }, // (F1) Daylight Fluorescent
            new object[] { 0.37208, 0.37529, 4230 }, // (F2) Cool White Fluorescent
            new object[] { 0.40910, 0.39430, 3450 }, // (F3) White Fluorescent
            new object[] { 0.44018, 0.40329, 2940 }, // (F4) Warm White Fluorescent
            new object[] { 0.31379, 0.34531, 6350 }, // (F5) Daylight Fluorescent
            new object[] { 0.37790, 0.38835, 4150 }, // (F6) Lite White Fluorescent
            new object[] { 0.31292, 0.32933, 6500 }, // (F7) D65 simulator, Daylight simulator
            new object[] { 0.34588, 0.35875, 5000 }, // (F8) D50 simulator, Sylvania F40 Design 50
            new object[] { 0.37417, 0.37281, 4150 }, // (F9) Cool White Deluxe Fluorescent
            new object[] { 0.34609, 0.35986, 5000 }, // (F10) Philips TL85, Ultralume 50
            new object[] { 0.38052, 0.37713, 4000 }, // (F11) Philips TL84, Ultralume 40
            new object[] { 0.43695, 0.40441, 3000 }, // (F12) Philips TL83, Ultralume 30
        };

        /// <summary>
        /// Data from: http://www.brucelindbloom.com/index.html?ColorCalculator.html
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        // Results are not precise enough.
        public static readonly IEnumerable<object[]> CCTData_Lindbloom = new []
        {
            new object[] { 0.585719, 0.393121, 1500 },
            new object[] { 0.526679, 0.413297, 2000 },
            new object[] { 0.476996, 0.413676, 2500 },
            new object[] { 0.436932, 0.404075, 3000 },
            new object[] { 0.405304, 0.390716, 3500 },
            new object[] { 0.380440, 0.376747, 4000 },
            new object[] { 0.345102, 0.351608, 5000 },
            new object[] { 0.322083, 0.331753, 6000 },
            new object[] { 0.306374, 0.316512, 7000 },
            new object[] { 0.295187, 0.304764, 8000 },
            new object[] { 0.286925, 0.295582, 9000 },
            new object[] { 0.280633, 0.288286, 10000 },
            new object[] { 0.275714, 0.282394, 11000 },
            new object[] { 0.271783, 0.277561, 12000 },
            new object[] { 0.271783, 0.277561, 12000 },
            new object[] { 0.268581, 0.273543, 13000 },
            new object[] { 0.265931, 0.270159, 14000 },
            new object[] { 0.263705, 0.267277, 15000 },
        };

        [Theory]
        [MemberData(nameof(CCTData_Wikipedia))]
        public void CCTFromChromaticity(double x, double y, double expectedCCT)
        {
            // arrange
            var chromaticity = new xyChromaticityCoordinates(x, y);
            var approximation = new CCTConverter();

            // action
            var cct = approximation.GetCCTOfChromaticity(chromaticity);

            // assert
            Debug.WriteLine($"CCT {cct} K (difference {Math.Abs(expectedCCT - cct)} K)");
            var deltaComparer = new DoubleDeltaComparer(66);
            Assert.Equal(cct, expectedCCT, deltaComparer);
        }

        [Theory]
        [MemberData(nameof(CCTData_Wikipedia))]
        public void ChromaticityFromCCT(double expectedX, double expectedY, double cct)
        {
            // arrange
            var approximation = new CCTConverter();

            // action
            var chromaticity = approximation.GetChromaticityOfCCT(cct);

            // assert
            var deltaComparer = new DoubleDeltaComparer(0.02);
            Assert.Equal(chromaticity.x, expectedX, deltaComparer);
            Assert.Equal(chromaticity.y, expectedY, deltaComparer);
        }
    }
}