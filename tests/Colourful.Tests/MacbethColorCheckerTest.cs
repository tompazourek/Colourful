using System;
using System.Collections.Generic;
using Xunit;

namespace Colourful.Tests
{
    public class MacbethColorCheckerTest
    {
        [Fact]
        public void TestColors()
        {
            Assert.Equal(24, MacbethColorChecker.Colors.Count);
            for (var i = 0; i < 24; i++)
            {
                var actualColor = MacbethColorChecker.Colors[i];
                RGBColor expectedColor;
                switch (i)
                {
                    case 0:
                        expectedColor = MacbethColorChecker.DarkSkin;
                        break;
                    case 1:
                        expectedColor = MacbethColorChecker.LightSkin;
                        break;
                    case 2:
                        expectedColor = MacbethColorChecker.BlueSky;
                        break;
                    case 3:
                        expectedColor = MacbethColorChecker.Foliage;
                        break;
                    case 4:
                        expectedColor = MacbethColorChecker.BlueFlower;
                        break;
                    case 5:
                        expectedColor = MacbethColorChecker.BluishGreen;
                        break;
                    case 6:
                        expectedColor = MacbethColorChecker.Orange;
                        break;
                    case 7:
                        expectedColor = MacbethColorChecker.PurplishBlue;
                        break;
                    case 8:
                        expectedColor = MacbethColorChecker.ModerateRed;
                        break;
                    case 9:
                        expectedColor = MacbethColorChecker.Purple;
                        break;
                    case 10:
                        expectedColor = MacbethColorChecker.YellowGreen;
                        break;
                    case 11:
                        expectedColor = MacbethColorChecker.OrangeYellow;
                        break;
                    case 12:
                        expectedColor = MacbethColorChecker.Blue;
                        break;
                    case 13:
                        expectedColor = MacbethColorChecker.Green;
                        break;
                    case 14:
                        expectedColor = MacbethColorChecker.Red;
                        break;
                    case 15:
                        expectedColor = MacbethColorChecker.Yellow;
                        break;
                    case 16:
                        expectedColor = MacbethColorChecker.Magenta;
                        break;
                    case 17:
                        expectedColor = MacbethColorChecker.Cyan;
                        break;
                    case 18:
                        expectedColor = MacbethColorChecker.White;
                        break;
                    case 19:
                        expectedColor = MacbethColorChecker.Neutral8;
                        break;
                    case 20:
                        expectedColor = MacbethColorChecker.Neutral6p5;
                        break;
                    case 21:
                        expectedColor = MacbethColorChecker.Neutral5;
                        break;
                    case 22:
                        expectedColor = MacbethColorChecker.Neutral3p5;
                        break;
                    case 23:
                        expectedColor = MacbethColorChecker.Black;
                        break;
                    default:
                        throw new Exception();
                }
                Assert.Equal(actualColor, expectedColor, new ColorVectorComparer(Comparer<double>.Default));
            }
        }
    }
}