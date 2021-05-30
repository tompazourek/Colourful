# xy chromaticity

## xy

- **Type:** `xyChromaticity`
- **Channels:**
  - **x** from 0 to 1
  - **y** from 0 to 1

The xy chromaticity space is unusual for two reasons. It's not an actual color space, as the xy coordinates don't fully specify a color, it says nothing about how bright the color actually is.

It also only has two channels *x* and *y*.

The chromaticity is often used to specify the color of the light without saying how bright it is. It can be used to specify a [white point of an illuminant](topic-illuminants.md) or [RGB working space primaries](spaces-rgb.md).

```csharp
// red
var c1 = new xyChromaticity(0.5736, 0.3209);

// white/gray/black
var c2 = new xyChromaticity(0.3127, 0.3290);
```

Note that in the example above, the colors white, gray, and black, all correspond to the same chromaticity. That is because these colors all have the same chromaticity, they only differ in luminance, which is what is missing.


## xyY

- **Type:** `xyYColor`
- **Channels:**
  - **x** from 0 to 1
  - **y** from 0 to 1
  - **Luminance** (Y) from 0 to 1

The xyY color space is just the xy chromaticity with the luminance channel Y added to it.

```csharp
// red
var c1 = new xyYColor(0.5736, 0.3209, 0.21);

// white
var c2 = new xyYColor(0.3127, 0.3290, 1);

// gray
var c3 = new xyYColor(0.3127, 0.3290, 0.21);

// black (chromaticity doesn't actually matter here)
var c4 = new xyYColor(0.3127, 0.3290, 0);
```


## Conversion strategy

When converting between `xyChromaticity` and any other color space, it always goes through the `xyYColor` first. When converting to xyY, Colourful uses the maximum luminance (channel Y) of 1. As a side-effect of this, it might mean that the resulting color falls outside of the gamut of another color space. If your values fall outside of the range, see the [dedicated page about clamping and normalizing intensity](topic-clamp.md). When converting from xyY to xy, the Y channel is simply discarded.

When converting between `xyYColor` and any other color space, it goes through the [XYZ color space](spaces-xyz.md).


## Convert between xy and RGB

Converting from [RGB color space](spaces-rgb.md) to xy chromaticity is fairly straightforward. What you might encounter is the need to specify the white point. In most cases, you'll probably want to use the same white point as your RGB working space does.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToxy = new ConverterBuilder().FromRGB(rgbWorkingSpace).Toxy(rgbWorkingSpace.WhitePoint).Build();
var outputXy = rgbToxy.Convert(inputRgb); // xy [x=0.57, y=0.32]
```

When converting back, the process is a bit more tricky. The xy chromaticity is missing the luminance information, so luminance 1 is used, which might make lots of colors fall outside of the range of RGB. To combat this, we use the [normalize intensity](topic-clamp.md) helper to fix the RGB color to maximum intensity within its gamut. This normalization is done in the [linear RGB color space](spaces-rgb.md), which means that there's one more conversion needed.

```csharp
var inputXy = new xyChromaticity(0.5736, 0.3209);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var xyToLinearRgb = new ConverterBuilder().Fromxy(rgbWorkingSpace.WhitePoint).ToLinearRGB(rgbWorkingSpace).Build();
var linearRgb = xyToLinearRgb.Convert(inputXy); // LinearRGB [R=4.09, G=0.16, B=0.24]
var normalizedLinearRgb = linearRgb.NormalizeIntensity(); // LinearRGB [R=1, G=0.04, B=0.06]

var linearRgbToRgb = new ConverterBuilder().FromLinearRGB(rgbWorkingSpace).ToRGB(rgbWorkingSpace).Build();
var outputRgb = linearRgbToRgb.Convert(normalizedLinearRgb); // RGB [R=1, G=0.22, B=0.27]
```


## Related links

- https://en.wikipedia.org/wiki/Chromaticity
- https://en.wikipedia.org/wiki/CIE_1931_color_space#CIE_xy_chromaticity_diagram_and_the_CIE_xyY_color_space
- http://www.brucelindbloom.com/index.html?Eqn_xyY_to_XYZ.html
