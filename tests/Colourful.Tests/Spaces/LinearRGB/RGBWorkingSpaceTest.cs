using System.Collections.Generic;
using Colourful.Internals;
using Colourful.Tests.Assertions;
using Xunit;
using static Colourful.RGBWorkingSpaces;

namespace Colourful.Tests;

/// <summary>
/// Tests equals on RGB working space.
/// </summary>
public class RGBWorkingSpaceTest
{
    private class AdobeRGB1998Duplicate : IRGBWorkingSpace
    {
        public ICompanding Companding => new GammaCompanding(gamma: 2.2);

        public XYZColor WhitePoint => Illuminants.D65;

        public RGBPrimaries Primaries => new RGBPrimaries(new xyChromaticity(x: 0.6400, y: 0.3300), new xyChromaticity(x: 0.2100, y: 0.7100), new xyChromaticity(x: 0.1500, y: 0.0600));
    }

    [Fact]
    public void DifferentWorkingSpace_IsNotEqual()
    {
        // arrange
        var x = sRGB;
        var y = AdobeRGB1998;

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
        var y = AdobeRGB1998;

        // act
        var equals = y.Equals(x);

        // assert
        Assert.True(equals);
    }

    [Fact]
    public void SameReference_IsEqual()
    {
        // arrange
        var x = sRGB;
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
        var x = sRGB;
        var y = sRGB;

        // act
        var equals = x.Equals(y);

        // assert
        Assert.True(equals);
    }

    [Fact]
    public void ComplexRGBWorkingSpaceEqualityTest()
    {
        var spaces1 = new List<IRGBWorkingSpace>
        {
            sRGB,
            sRGBSimplified,
            Rec709,
            Rec2020,
            ECIRGBv2,
            AdobeRGB1998,
            ApplesRGB,
            BestRGB,
            BetaRGB,
            BruceRGB,
            CIERGB,
            ColorMatchRGB,
            DonRGB4,
            EktaSpacePS5,
            NTSCRGB,
            PALSECAMRGB,
            ProPhotoRGB,
            SMPTECRGB,
            WideGamutRGB,
            new RGBWorkingSpace(Illuminants.C, new GammaCompanding(15.77), new RGBPrimaries(new xyChromaticity(1, 2), new xyChromaticity(3, 4), new xyChromaticity(5, 6))),
        };

        // same as above (separate instance where non-static)
        var spaces2 = new List<IRGBWorkingSpace>
        {
            sRGB,
            sRGBSimplified,
            Rec709,
            Rec2020,
            ECIRGBv2,
            AdobeRGB1998,
            ApplesRGB,
            BestRGB,
            BetaRGB,
            BruceRGB,
            CIERGB,
            ColorMatchRGB,
            DonRGB4,
            EktaSpacePS5,
            NTSCRGB,
            PALSECAMRGB,
            ProPhotoRGB,
            SMPTECRGB,
            WideGamutRGB,
            new RGBWorkingSpace(Illuminants.C, new GammaCompanding(15.77), new RGBPrimaries(new xyChromaticity(1, 2), new xyChromaticity(3, 4), new xyChromaticity(5, 6))),
        };

        // check that same are the same and different are different
        for (var i = 0; i < spaces1.Count; i++)
        {
            var s1 = spaces1[i];

            for (var j = 0; j < spaces2.Count; j++)
            {
                var s2 = spaces2[j];

                if (i == j)
                {
                    CustomAssert.EqualsWithHashCode(s1, s2);
                }
                else
                {
                    CustomAssert.NotEqualsWithHashCode(s1, s2);
                }
            }

            CustomAssert.NotEqualsWithHashCode(s1, (IRGBWorkingSpace)null);
        }
    }
}
