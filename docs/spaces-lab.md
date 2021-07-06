# Lab color spaces

## Lab

- **Type:** `LabColor`
- **Channels:**
  - **L<span>*</span>** (lightness) usually between 0% and 100%
  - **a<span>*</span>** green-red chromaticity mostly between -100 and 100 (not always)
  - **b<span>*</span>** blue-yellow chromaticity mostly between -100 and 100 (not always)

The CIE L\*a\*b\* (1976) *(CIELAB)* color space (similarly to [CIELUV](spaces-luv.md)) is designed to have perceptual uniformity, which means that distances in the color space should more closely correspond to how humans perceive the color differences. Although the perceptual uniformity is known to have its issues.

The Lab color space is often used in the printing industry.

```csharp
// red
var c1 = new LabColor(53.9, 70.46, 41);

// white
var c2 = new LabColor(100, 0, 0);

// gray
var c3 = new LabColor(53.9, 0, 0);

// black
var c4 = new LabColor(0, 0, 0);
```


## LChab

- **Type:** `LChabColor`
- **Channels:**
  - **L<span>*</span>** (lightness) usually between 0% and 100%
  - **C<span>*</span>** (chroma) usually between 0% and 100%
  - **h<span>°</span>** (hue in degrees) between 0° and 360°

The CIE L\*C\*h°<sub>ab</sub> *(CIELCH)* color space is a [cylindrical representation](topic-cylindrical-spaces.md) of the Lab color space.

```csharp
// red
var c1 = new LChabColor(53.04, 81.52, 30.2);

// white
var c2 = new LChabColor(100, 0, 0);

// gray
var c3 = new LChabColor(53.9, 0, 0);

// black
var c4 = new LChabColor(0, 0, 0);
```


## Hunter Lab

- **Type:** `HunterLabColor`
- **Channels:**
  - **L** (lightness) usually between 0% and 100%
  - **a** green-red chromaticity mostly between -100 and 100 (not always)
  - **b** blue-yellow chromaticity mostly between -100 and 100 (not always)

The Hunter Lab color space is an older colorspace than CIE L\*a\*b\* defined by Richard S. Hunter. It has similarities to CIE L\*a\*b\*, but uses different conversion formulas. Sometimes when you see just "Lab" without the asterisks ("L\*a\*b\*"), it might actually be referring to Hunter Lab instead of CIELAB.

Compared to CIELAB which usually uses D50 (and sometimes D65), Hunter Lab uses the C illuminant.


```csharp
// red
var c1 = new HunterLabColor(46.1, 67.57, 22.58);

// white
var c2 = new HunterLabColor(100, 0, 0);

// gray
var c3 = new HunterLabColor(46.26, 0, 0);

// black
var c4 = new HunterLabColor(0, 0, 0);
```



## Conversion strategy

When converting between `LabColor` and any other color space, it always goes through `XYZColor` first.

Only exception is the `LChabColor` where we go through `LabColor` first.

The `LabColor` values are relative to the [white point](topic-illuminants.md) used. Which means that when converting between color spaces, you'll need to specify which white point you want the source/target color to be relative to. In case the white points in source and target are different, [chromatic adaptation](topic-conversion.md#chromatic-adaptation) will be performed.

In most cases, the CIELAB values you will encounter use the D50 white point (which is different to D65 white point used by [sRGB](spaces-rgb.md)).

For `HunterLabColor` and any other color space, it always goes through `XYZColor` first (same strategy as `LabColor`, but different formulas). If you use Hunter Lab, you'll probably want to use the C white point in the Hunter Lab space.


## How to convert between Lab and RGB?

In this example, the conversion from [RGB color space](spaces-rgb.md) to Lab is demonstrated. We use the sRGB working space and change to the [D50 white point](topic-illuminants.md) for Lab.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToLab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLab(Illuminants.D50).Build();
var outputLab = rgbToLab.Convert(inputRgb); // Lab [L=53.9, a=70.46, b=41]
```

The reverse is similar:

```csharp
var inputLab = new LabColor(53.9, 70.46, 41);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var labToRgb = new ConverterBuilder().FromLab(Illuminants.D50).ToRGB(rgbWorkingSpace).Build();
var outputRgb = labToRgb.Convert(inputLab); // RGB [R=0.94, G=0.2, B=0.25]
```


## How to convert between LChab and RGB?

Conversions between LChab and RGB are similar to Lab above.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToLChab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLChab(Illuminants.D50).Build();
var outputLChab = rgbToLChab.Convert(inputRgb); // LChab [L=53.9, C=81.52, h=30.2]
```

The reverse:

```csharp
var inputLChab = new LChabColor(53.9, 81.52, 30.2);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var lChabToRgb = new ConverterBuilder().FromLChab(Illuminants.D50).ToRGB(rgbWorkingSpace).Build();
var outputRgb = lChabToRgb.Convert(inputLChab); // RGB [R=0.94, G=0.2, B=0.25]
```


## How to convert between Hunter Lab and RGB?

We use the sRGB working space and change to the [C white point](topic-illuminants.md) commonly used by Hunter Lab.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToHunterLab = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToHunterLab(Illuminants.C).Build();
var outputHunterLab = rgbToHunterLab.Convert(inputRgb); // HunterLab [L=46.1, a=67.57, b=22.58]
```

The reverse:

```csharp
var inputHunterLab = new HunterLabColor(46.1, 67.57, 22.58);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var hunterLabToRgb = new ConverterBuilder().FromHunterLab(Illuminants.C).ToRGB(rgbWorkingSpace).Build();
var outputRgb = hunterLabToRgb.Convert(inputHunterLab); // RGB [R=0.94, G=0.2, B=0.25]
```


## Compute color difference using Lab colors

Many of the [color difference algorithms in Colourful](topic-color-difference.md) actually use the Lab color space for their inputs. These include:

- CIE Delta-E (1976)
- CMC l:c (1984)
- CIE Delta-E (1994)
- CIE Delta-E (2000)


## Related links

- https://en.wikipedia.org/wiki/CIELAB_color_space
- [Cylindrical color spaces](topic-cylindrical-spaces.md)
- [Color difference](topic-color-difference.md)
- http://www.brucelindbloom.com/index.html?Eqn_Lab_to_XYZ.html
- http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_Lab.html
