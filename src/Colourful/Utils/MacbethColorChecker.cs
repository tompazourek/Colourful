using static Colourful.RGBColor;
using ColorList = System.Collections.Generic.IReadOnlyList<Colourful.RGBColor>;

namespace Colourful
{
    /// <summary>
    /// Colors of the Macbeth ColorChecker.
    /// Assume that the RGB colors are in sRGB working space.
    /// </summary>
    /// <remarks>
    /// Values obtained from: http://xritephoto.com/documents/literature/en/ColorData-1p_EN.pdf
    /// </remarks>
    public static class MacbethColorChecker
    {
        /// <summary>
        /// Dark skin (color #1).
        /// </summary>
        public static readonly RGBColor DarkSkin = FromRGB8Bit(r: 115, g: 82, b: 68);

        /// <summary>
        /// Light skin (color #2).
        /// </summary>
        public static readonly RGBColor LightSkin = FromRGB8Bit(r: 194, g: 150, b: 130);

        /// <summary>
        /// Blue sky (color #3).
        /// </summary>
        public static readonly RGBColor BlueSky = FromRGB8Bit(r: 98, g: 122, b: 157);

        /// <summary>
        /// Foliage (color #4).
        /// </summary>
        public static readonly RGBColor Foliage = FromRGB8Bit(r: 87, g: 108, b: 67);

        /// <summary>
        /// Blue flower (color #5).
        /// </summary>
        public static readonly RGBColor BlueFlower = FromRGB8Bit(r: 133, g: 128, b: 177);

        /// <summary>
        /// Bluish green (color #6).
        /// </summary>
        public static readonly RGBColor BluishGreen = FromRGB8Bit(r: 103, g: 189, b: 170);

        /// <summary>
        /// Orange (color #7).
        /// </summary>
        public static readonly RGBColor Orange = FromRGB8Bit(r: 214, g: 126, b: 44);

        /// <summary>
        /// Purplish blue (color #8).
        /// </summary>
        public static readonly RGBColor PurplishBlue = FromRGB8Bit(r: 80, g: 91, b: 166);

        /// <summary>
        /// Moderate red (color #9).
        /// </summary>
        public static readonly RGBColor ModerateRed = FromRGB8Bit(r: 193, g: 90, b: 99);

        /// <summary>
        /// Purple (color #10).
        /// </summary>
        public static readonly RGBColor Purple = FromRGB8Bit(r: 94, g: 60, b: 108);

        /// <summary>
        /// Yellow green (color #11).
        /// </summary>
        public static readonly RGBColor YellowGreen = FromRGB8Bit(r: 157, g: 188, b: 64);

        /// <summary>
        /// Orange Yellow (color #12).
        /// </summary>
        public static readonly RGBColor OrangeYellow = FromRGB8Bit(r: 224, g: 163, b: 46);

        /// <summary>
        /// Blue (color #13).
        /// </summary>
        public static readonly RGBColor Blue = FromRGB8Bit(r: 56, g: 61, b: 150);

        /// <summary>
        /// Green (color #14).
        /// </summary>
        public static readonly RGBColor Green = FromRGB8Bit(r: 70, g: 148, b: 73);

        /// <summary>
        /// Red (color #15).
        /// </summary>
        public static readonly RGBColor Red = FromRGB8Bit(r: 175, g: 54, b: 60);

        /// <summary>
        /// Yellow (color #16).
        /// </summary>
        public static readonly RGBColor Yellow = FromRGB8Bit(r: 231, g: 199, b: 31);

        /// <summary>
        /// Magenta (color #17).
        /// </summary>
        public static readonly RGBColor Magenta = FromRGB8Bit(r: 187, g: 86, b: 149);

        /// <summary>
        /// Cyan (color #18).
        /// </summary>
        public static readonly RGBColor Cyan = FromRGB8Bit(r: 8, g: 133, b: 161);

        /// <summary>
        /// White (color #19).
        /// </summary>
        public static readonly RGBColor White = FromRGB8Bit(r: 243, g: 243, b: 242);

        /// <summary>
        /// Neutral 8 (color #20).
        /// </summary>
        public static readonly RGBColor Neutral8 = FromRGB8Bit(r: 200, g: 200, b: 200);

        /// <summary>
        /// Neutral 6.5 (color #21).
        /// </summary>
        public static readonly RGBColor Neutral6p5 = FromRGB8Bit(r: 160, g: 160, b: 160);

        /// <summary>
        /// Neutral 5 (color #22).
        /// </summary>
        public static readonly RGBColor Neutral5 = FromRGB8Bit(r: 122, g: 122, b: 121);

        /// <summary>
        /// Neutral 3.5 (color #23).
        /// </summary>
        public static readonly RGBColor Neutral3p5 = FromRGB8Bit(r: 85, g: 85, b: 85);

        /// <summary>
        /// Black (color #24).
        /// </summary>
        public static readonly RGBColor Black = FromRGB8Bit(r: 52, g: 52, b: 52);

        /// <summary>
        /// Array of 24 colors of the Macbeth ColorChecker.
        /// </summary>
        public static readonly ColorList Colors = new[] { DarkSkin, LightSkin, BlueSky, Foliage, BlueFlower, BluishGreen, Orange, PurplishBlue, ModerateRed, Purple, YellowGreen, OrangeYellow, Blue, Green, Red, Yellow, Magenta, Cyan, White, Neutral8, Neutral6p5, Neutral5, Neutral3p5, Black };
    }
}