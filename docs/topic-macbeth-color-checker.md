# Macbeth ColorChecker chart

The Colourful library contains predefined constants for the 24 individual colors of the [Macbeth ColorChecker Color Rendition Chart](https://en.wikipedia.org/wiki/ColorChecker).

These can be accessed via a static class called `MacbethColorChecker`. The colors are defined in the [sRGB color space](spaces-rgb.md#rgb-working-spaces). This means that if you want to [convert](topic-conversion.md) the colors into a different space, use the sRGB as the input working space.

You can access them via their names:

```csharp
RGBColor color01 = MacbethColorChecker.DarkSkin;
RGBColor color02 = MacbethColorChecker.LightSkin;
RGBColor color03 = MacbethColorChecker.BlueSky;
RGBColor color04 = MacbethColorChecker.Foliage;
RGBColor color05 = MacbethColorChecker.BlueFlower;
RGBColor color06 = MacbethColorChecker.BluishGreen;
RGBColor color07 = MacbethColorChecker.Orange;
RGBColor color08 = MacbethColorChecker.PurplishBlue;
RGBColor color09 = MacbethColorChecker.ModerateRed;
RGBColor color10 = MacbethColorChecker.Purple;
RGBColor color11 = MacbethColorChecker.YellowGreen;
RGBColor color12 = MacbethColorChecker.OrangeYellow;
RGBColor color13 = MacbethColorChecker.Blue;
RGBColor color14 = MacbethColorChecker.Green;
RGBColor color15 = MacbethColorChecker.Red;
RGBColor color16 = MacbethColorChecker.Yellow;
RGBColor color17 = MacbethColorChecker.Magenta;
RGBColor color18 = MacbethColorChecker.Cyan;
RGBColor color19 = MacbethColorChecker.White;
RGBColor color10 = MacbethColorChecker.Neutral8;
RGBColor color21 = MacbethColorChecker.Neutral6p5;
RGBColor color22 = MacbethColorChecker.Neutral5;
RGBColor color23 = MacbethColorChecker.Neutral3p5;
RGBColor color24 = MacbethColorChecker.Black;
```

Alternatively, you can also get all those colors in a list:

```csharp
IReadOnlyList<RGBColor> colors = MacbethColorChecker.Colors;
```
