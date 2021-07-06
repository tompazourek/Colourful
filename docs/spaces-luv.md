# Luv color spaces

## Luv

- **Type:** `LuvColor`
- **Channels:**
  - **L<span>*</span>** (lightness) usually between 0% and 100%
  - **u<span>*</span>** chromaticity mostly between -100% and 100% (not always)
  - **v<span>*</span>** chromaticity mostly between -100% and 100% (not always)

The CIE L\*u\*v\* (1976) *(CIELUV)* color space (similarly to [CIELAB](spaces-lab.md)) is designed to have perceptual uniformity, which means that distances in the color space should more closely correspond to how humans perceive the color differences.

```csharp
// red
var c1 = new LuvColor(53.04, 140.97, 26.21);

// white
var c2 = new LuvColor(100, 0, 0);

// gray
var c3 = new LuvColor(53.39, 0, 0);

// black
var c4 = new LuvColor(0, 0, 0);
```


## LChuv

- **Type:** `LChuvColor`
- **Channels:**
  - **L<span>*</span>** (lightness) usually between 0% and 100%
  - **C<span>*</span>** (chroma) usually between 0% and 100%
  - **h<span>°</span>** (hue in degrees) between 0° and 360°

The CIE L\*C\*h°<sub>uv</sub> *(CIELCH)* color space is a [cylindrical representation](topic-cylindrical-spaces.md) of the Luv color space. Sometimes it's also known as CIE HLC<sub>uv</sub>.

```csharp
// red
var c1 = new LChuvColor(53.04, 143.39, 10.53);

// white
var c2 = new LChuvColor(100, 0, 0);

// gray
var c3 = new LChuvColor(53.39, 0, 0);

// black
var c4 = new LChuvColor(0, 0, 0);
```


## Conversion strategy

When converting between `LuvColor` and any other color space, it always goes through `XYZColor` first.

Only exception is the `LChuvColor` where we go through `LuvColor` first.

The `LuvColor` values are relative to the [white point](topic-illuminants.md) used. Which means that when converting between color spaces, you'll need to specify which white point you want the source/target color to be relative to. In case the white points in source and target are different, [chromatic adaptation](topic-conversion.md#chromatic-adaptation) will be performed.


## How to convert between Luv and RGB?

In this example, the conversion from [RGB color space](spaces-rgb.md) to Luv is demonstrated. We use the sRGB working space and keep the [D65 white point](topic-illuminants.md).

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToLuv = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLuv(rgbWorkingSpace.WhitePoint).Build();
var outputLuv = rgbToLuv.Convert(inputRgb); // Luv [L=53.04, u=140.97, v=26.21]
```

The reverse is similar:

```csharp
var inputLuv = new LuvColor(53.04, 140.97, 26.21);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var luvToRgb = new ConverterBuilder().FromLuv(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = luvToRgb.Convert(inputLuv); // RGB [R=0.94, G=0.2, B=0.25]
```


## How to convert between LChuv and RGB?

Conversions between LChuv and RGB are similar to Luv above.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToLChuv = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLChuv(rgbWorkingSpace.WhitePoint).Build();
var outputLChuv = rgbToLChuv.Convert(inputRgb); // LChuv [L=53.04, C=143.39, h=10.53]
```

The reverse:

```csharp
var inputLChuv = new LChuvColor(53.04, 143.39, 10.53);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var lChuvToRgb = new ConverterBuilder().FromLChuv(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = lChuvToRgb.Convert(inputLChuv); // RGB [R=0.94, G=0.2, B=0.25]
```


## Related links

- https://en.wikipedia.org/wiki/CIELUV
- [Cylindrical color spaces](topic-cylindrical-spaces.md)
- [Euclidean distance](topic-color-difference.md#euclidean-distance)
- http://www.brucelindbloom.com/index.html?Eqn_Luv_to_XYZ.html
- http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Luv.html
