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
        public static readonly RGBColor DarkSkin = RGBColor.FromRGB8bit(red: 115, green: 82, blue: 68);

        /// <summary>
        /// Light skin (color #2)
        /// </summary>
        public static readonly RGBColor LightSkin = RGBColor.FromRGB8bit(red: 194, green: 150, blue: 130);

        /// <summary>
        /// Blue sky (color #3)
        /// </summary>
        public static readonly RGBColor BlueSky = RGBColor.FromRGB8bit(red: 98, green: 122, blue: 157);

        /// <summary>
        /// Foliage (color #4)
        /// </summary>
        public static readonly RGBColor Foliage = RGBColor.FromRGB8bit(red: 87, green: 108, blue: 67);

        /// <summary>
        /// Blue flower (color #5)
        /// </summary>
        public static readonly RGBColor BlueFlower = RGBColor.FromRGB8bit(red: 133, green: 128, blue: 177);

        /// <summary>
        /// Bluish green (color #6)
        /// </summary>
        public static readonly RGBColor BluishGreen = RGBColor.FromRGB8bit(red: 103, green: 189, blue: 170);

        /// <summary>
        /// Orange (color #7)
        /// </summary>
        public static readonly RGBColor Orange = RGBColor.FromRGB8bit(red: 214, green: 126, blue: 44);

        /// <summary>
        /// Purplish blue (color #8)
        /// </summary>
        public static readonly RGBColor PurplishBlue = RGBColor.FromRGB8bit(red: 80, green: 91, blue: 166);

        /// <summary>
        /// Moderate red (color #9)
        /// </summary>
        public static readonly RGBColor ModerateRed = RGBColor.FromRGB8bit(red: 193, green: 90, blue: 99);

        /// <summary>
        /// Purple (color #10)
        /// </summary>
        public static readonly RGBColor Purple = RGBColor.FromRGB8bit(red: 94, green: 60, blue: 108);

        /// <summary>
        /// Yellow green (color #11)
        /// </summary>
        public static readonly RGBColor YellowGreen = RGBColor.FromRGB8bit(red: 157, green: 188, blue: 64);

        /// <summary>
        /// Orange Yellow (color #12)
        /// </summary>
        public static readonly RGBColor OrangeYellow = RGBColor.FromRGB8bit(red: 224, green: 163, blue: 46);

        /// <summary>
        /// Blue (color #13)
        /// </summary>
        public static readonly RGBColor Blue = RGBColor.FromRGB8bit(red: 56, green: 61, blue: 150);

        /// <summary>
        /// Green (color #14)
        /// </summary>
        public static readonly RGBColor Green = RGBColor.FromRGB8bit(red: 70, green: 148, blue: 73);

        /// <summary>
        /// Red (color #15)
        /// </summary>
        public static readonly RGBColor Red = RGBColor.FromRGB8bit(red: 175, green: 54, blue: 60);

        /// <summary>
        /// Yellow (color #16)
        /// </summary>
        public static readonly RGBColor Yellow = RGBColor.FromRGB8bit(red: 231, green: 199, blue: 31);

        /// <summary>
        /// Magenta (color #17)
        /// </summary>
        public static readonly RGBColor Magenta = RGBColor.FromRGB8bit(red: 187, green: 86, blue: 149);

        /// <summary>
        /// Cyan (color #18)
        /// </summary>
        public static readonly RGBColor Cyan = RGBColor.FromRGB8bit(red: 8, green: 133, blue: 161);

        /// <summary>
        /// White (color #19)
        /// </summary>
        public static readonly RGBColor White = RGBColor.FromRGB8bit(red: 243, green: 243, blue: 242);

        /// <summary>
        /// Neutral 8 (color #20)
        /// </summary>
        public static readonly RGBColor Neutral8 = RGBColor.FromRGB8bit(red: 200, green: 200, blue: 200);

        /// <summary>
        /// Neutral 6.5 (color #21)
        /// </summary>
        public static readonly RGBColor Neutral6p5 = RGBColor.FromRGB8bit(red: 160, green: 160, blue: 160);

        /// <summary>
        /// Neutral 5 (color #22)
        /// </summary>
        public static readonly RGBColor Neutral5 = RGBColor.FromRGB8bit(red: 122, green: 122, blue: 121);

        /// <summary>
        /// Neutral 3.5 (color #23)
        /// </summary>
        public static readonly RGBColor Neutral3p5 = RGBColor.FromRGB8bit(red: 85, green: 85, blue: 85);

        /// <summary>
        /// Black (color #24)
        /// </summary>
        public static readonly RGBColor Black = RGBColor.FromRGB8bit(red: 52, green: 52, blue: 52);

        /// <summary>
        /// Array of 24 colors of the Macbeth ColorChecker
        /// </summary>
        public static readonly ColorList Colors = new[] { DarkSkin, LightSkin, BlueSky, Foliage, BlueFlower, BluishGreen, Orange, PurplishBlue, ModerateRed, Purple, YellowGreen, OrangeYellow, Blue, Green, Red, Yellow, Magenta, Cyan, White, Neutral8, Neutral6p5, Neutral5, Neutral3p5, Black };
    }
}