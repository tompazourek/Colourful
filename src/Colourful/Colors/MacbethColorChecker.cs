using ColorList = System.Collections.Generic.IReadOnlyList<Colourful.RGBColor>;

namespace Colourful
{
    /// <summary>
    /// Colors of the Macbeth ColorChecker
    /// </summary>
    /// <remarks>
    /// Values obtained from: http://xritephoto.com/documents/literature/en/ColorData-1p_EN.pdf
    /// </remarks>
    public static class MacbethColorChecker
    {
        /// <summary>
        /// Dark skin (color #1)
        /// </summary>
        public static readonly RGBColor DarkSkin = RGBColor.FromRGB8bit(115, 82, 68, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Light skin (color #2)
        /// </summary>
        public static readonly RGBColor LightSkin = RGBColor.FromRGB8bit(194, 150, 130, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Blue sky (color #3)
        /// </summary>
        public static readonly RGBColor BlueSky = RGBColor.FromRGB8bit(98, 122, 157, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Foliage (color #4)
        /// </summary>
        public static readonly RGBColor Foliage = RGBColor.FromRGB8bit(87, 108, 67, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Blue flower (color #5)
        /// </summary>
        public static readonly RGBColor BlueFlower = RGBColor.FromRGB8bit(133, 128, 177, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Bluish green (color #6)
        /// </summary>
        public static readonly RGBColor BluishGreen = RGBColor.FromRGB8bit(103, 189, 170, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Orange (color #7)
        /// </summary>
        public static readonly RGBColor Orange = RGBColor.FromRGB8bit(214, 126, 44, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Purplish blue (color #8)
        /// </summary>
        public static readonly RGBColor PurplishBlue = RGBColor.FromRGB8bit(80, 91, 166, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Moderate red (color #9)
        /// </summary>
        public static readonly RGBColor ModerateRed = RGBColor.FromRGB8bit(193, 90, 99, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Purple (color #10)
        /// </summary>
        public static readonly RGBColor Purple = RGBColor.FromRGB8bit(94, 60, 108, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Yellow green (color #11)
        /// </summary>
        public static readonly RGBColor YellowGreen = RGBColor.FromRGB8bit(157, 188, 64, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Orange Yellow (color #12)
        /// </summary>
        public static readonly RGBColor OrangeYellow = RGBColor.FromRGB8bit(224, 163, 46, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Blue (color #13)
        /// </summary>
        public static readonly RGBColor Blue = RGBColor.FromRGB8bit(56, 61, 150, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Green (color #14)
        /// </summary>
        public static readonly RGBColor Green = RGBColor.FromRGB8bit(70, 148, 73, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Red (color #15)
        /// </summary>
        public static readonly RGBColor Red = RGBColor.FromRGB8bit(175, 54, 60, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Yellow (color #16)
        /// </summary>
        public static readonly RGBColor Yellow = RGBColor.FromRGB8bit(231, 199, 31, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Magenta (color #17)
        /// </summary>
        public static readonly RGBColor Magenta = RGBColor.FromRGB8bit(187, 86, 149, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Cyan (color #18)
        /// </summary>
        public static readonly RGBColor Cyan = RGBColor.FromRGB8bit(8, 133, 161, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// White (color #19)
        /// </summary>
        public static readonly RGBColor White = RGBColor.FromRGB8bit(243, 243, 242, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Neutral 8 (color #20)
        /// </summary>
        public static readonly RGBColor Neutral8 = RGBColor.FromRGB8bit(200, 200, 200, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Neutral 6.5 (color #21)
        /// </summary>
        public static readonly RGBColor Neutral6p5 = RGBColor.FromRGB8bit(160, 160, 160, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Neutral 5 (color #22)
        /// </summary>
        public static readonly RGBColor Neutral5 = RGBColor.FromRGB8bit(122, 122, 121, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Neutral 3.5 (color #23)
        /// </summary>
        public static readonly RGBColor Neutral3p5 = RGBColor.FromRGB8bit(85, 85, 85, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Black (color #24)
        /// </summary>
        public static readonly RGBColor Black = RGBColor.FromRGB8bit(52, 52, 52, RGBWorkingSpaces.sRGB);

        /// <summary>
        /// Array of 24 colors of the Macbeth ColorChecker
        /// </summary>
        public static readonly ColorList Colors = new[] { DarkSkin, LightSkin, BlueSky, Foliage, BlueFlower, BluishGreen, Orange, PurplishBlue, ModerateRed, Purple, YellowGreen, OrangeYellow, Blue, Green, Red, Yellow, Magenta, Cyan, White, Neutral8, Neutral6p5, Neutral5, Neutral3p5, Black };
    }
}