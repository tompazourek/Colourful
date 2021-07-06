# J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> color spaces

## Jzazbz

- **Type:** `JzazbzColor`
- **Channels:**
  - **J<sub>z</sub>** (lightness) usually between 0 and 1
  - **a<sub>z</sub>** green-red chromaticity usually between -1 and 1
  - **b<sub>z</sub>** blue-yellow chromaticity usually between -1 and 1

The J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> *(Safdar & al., 2017)* is a novel color space designed for perceptual uniformity, high dynamic range, and wide gamut.

```csharp
// red
var c1 = new JzazbzColor(0.6004, 0.1864, 0.1541);

// white (D65)
var c2 = new JzazbzColor(0.9886, -0.0002, -0.0001);

// gray (D65)
var c3 = new JzazbzColor(0.5446, -0.0002, -0.0001);

// black
var c4 = new JzazbzColor(0, 0, 0);
```


## JzCzhz

- **Type:** `JzCzhzColor`
- **Channels:**
  - **J<sub>z</sub>** (lightness) usually between 0 and 1
  - **C<sub>z</sub>** (chroma) usually between 0 and 1
  - **h<sub>z</sub>** (hue in degrees) between 0° and 360°

The J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> color space is a [cylindrical representation](topic-cylindrical-spaces.md) of the J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> color space.

```csharp
// red
var c1 = new JzCzhzColor(0.6004, 0.2418, 39.58);

// white (D65)
var c2 = new JzCzhzColor(0.9886, 0.0003, 211.6);

// gray (D65)
var c3 = new JzCzhzColor(0.5446, 0.0003, 211.6);

// black
var c4 = new JzCzhzColor(0, 0, 0);
```


## Conversion strategy

When converting between `JzazbzColor` and any other color space, it always goes through `XYZColor` first.

Only exception is the `JzCzhzColor` where we go through `JzazbzColor` first.

The `JzazbzColor` values are relative to the [white point](topic-illuminants.md) used. Which means that when converting between color spaces, you'll need to specify which white point you want the source/target color to be relative to. In case the white points in source and target are different, [chromatic adaptation](topic-conversion.md#chromatic-adaptation) will be performed.

In most cases, you'll want to use the D65 white point when operating in J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> and J<sub>z</sub>C<sub>z</sub>h<sub>z</sub>.


## How to convert between Jzazbz and RGB?

In this example, the conversion from [RGB color space](spaces-rgb.md) to Jzazbz is demonstrated. We use the sRGB working space and keep the [D65 white point](topic-illuminants.md).

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToJzazbz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToJzazbz(rgbWorkingSpace.WhitePoint).Build();
var outputJzazbz = rgbToJzazbz.Convert(inputRgb); // Jzazbz [Jz=0.6004, az=0.1864, bz=0.1541]
```

The reverse is similar:

```csharp
var inputJzazbz = new JzazbzColor(0.6004, 0.1864, 0.1541);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var jzazbzToRgb = new ConverterBuilder().FromJzazbz(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = jzazbzToRgb.Convert(inputJzazbz); // RGB [R=0.94, G=0.2, B=0.25]
```


## How to convert between JzCzhz and RGB?

Conversions between JzCzhz and RGB are similar to Jzazbz above.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToJzCzhz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToJzCzhz(rgbWorkingSpace.WhitePoint).Build();
var outputJzCzhz = rgbToJzCzhz.Convert(inputRgb); // JzCzhz [Jz=0.6004, Cz=0.2418, hz=39.58]
```

The reverse:

```csharp
var inputJzCzhz = new JzCzhzColor(0.6004, 0.2418, 39.58);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var jzCzhzToRgb = new ConverterBuilder().FromJzCzhz(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = jzCzhzToRgb.Convert(inputJzCzhz); // RGB [R=0.94, G=0.2, B=0.25]
```


## J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> color difference

The J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> color space can also be used with the [J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> Delta-E<sub>z</sub> color difference algorithm](topic-color-difference.md).


## Related links

- https://observablehq.com/@jrus/jzazbz
- [Cylindrical color spaces](topic-cylindrical-spaces.md)
