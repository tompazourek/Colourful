using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Colourful.Conversion;
using NUnit.Framework;
using DataRow = Colourful.Tests.ColorCheckerCalculatorData.Row;

namespace Colourful.Tests
{
    [TestFixture]
    public class ColorCheckerCalculatorTest
    {
        private static readonly DataRow[] TestData = ColorCheckerCalculatorData.GetData().ToArray();
        private readonly ColourfulConverter _converter;

        public ColorCheckerCalculatorTest()
        {
            _converter = new ColourfulConverter
            {
                WhitePoint = Illuminants.C,
                TargetRGBWorkingSpace = RGBWorkingSpaces.sRGB,
                TargetLabWhitePoint = Illuminants.C,
                TargetLuvWhitePoint = Illuminants.C,
                ChromaticAdaptation = new VonKriesChromaticAdaptation()
            };
        }

        private void RethrowException(AssertionException ex, DataRow row)
        {
            throw new AssertionException(string.Format("[{0}]\n{1}", row.Name, ex.Message), ex);
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_XYZ(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedXYZ = row.GetXYZColor();
            var actualXYZ = _converter.ToXYZ(inputLab);
            try
            {
                Assert.That(actualXYZ, Is.EqualTo(expectedXYZ)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.000001))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_RGB(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedRGB = row.GetRGBColor();
            var actualRGB = _converter.ToRGB(inputLab);
            try
            {
                Assert.That(actualRGB, Is.EqualTo(expectedRGB)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.00912))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_xyY(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedxyY = row.GetxyYColor();
            var actualxyY = _converter.ToxyY(inputLab);
            try
            {
                Assert.That(actualxyY, Is.EqualTo(expectedxyY)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.000001))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_LChab(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLChab = row.GetLChabColor();
            var actualLChab = _converter.ToLChab(inputLab);
            try
            {
                Assert.That(actualLChab, Is.EqualTo(expectedLChab)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.00017))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_LChuv(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLChuv = row.GetLChuvColor();
            var actualLChuv = _converter.ToLChuv(inputLab);
            try
            {
                Assert.That(actualLChuv, Is.EqualTo(expectedLChuv)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.00022))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }

        [TestCaseSource(nameof(TestData))]
        public void Convert_Lab_to_Luv(DataRow row)
        {
            var inputLab = row.GetLabColor();
            var expectedLuv = row.GetLuvColor();
            var actualLuv = _converter.ToLuv(inputLab);
            try
            {
                Assert.That(actualLuv, Is.EqualTo(expectedLuv)
                    .Using(new ColorVectorComparer(new DoubleDeltaComparer(0.00000105))));
            }
            catch (AssertionException ex)
            {
                RethrowException(ex, row);
            }
        }
    }
}