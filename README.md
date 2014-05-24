Colourful .NET
==============

Open source .NET library for working with color spaces. Still in **early phase of development**.

**Author:** [Tomáš Pažourek](https://github.com/tompazourek)

Usage
-----

### Color conversion

```csharp
RGBColor input = new RGBColor(1, 0, 0);

var converter = new ColorConverter { WhitePoint = Illuminants.D65 };

XYZColor output = converter.ToXYZ(input);
```

The `ColorConverter` facade can convert **from any of the supported color spaces to any other color space**.

It **always performs the chromatic adaptation** if the input and output color space white points are different.

### Chromatic adaptation

The adaptation can be also performed alone (e.g. from CIELAB D50 to CIELAB D65).


```csharp
LabColor input = new LabColor(10, 20, 30, Illuminants.D50);

var converter = new ColorConverter { TargetLabWhitePoint = Illuminants.D65 };

LabColor output = converter.Adapt(input);
```

Adaptation can also convert from one RGB working space to another (e.g. from sRGB to Adobe RGB).

```csharp
RGBColor input = new RGBColor(Color.Yellow, RGBWorkingSpaces.sRGB);

var converter = new ColorConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.AdobeRGB1998 };

RGBColor output = converter.Adapt(input);
```

### CCT approximation

Colourful also supports computing **correlated color temperature (CCT)** from chromaticity and computing chromaticity from CCT. Altough these are just approximations with low precision.

```csharp
var converter = new CCTConverter();

ChromaticityCoordinates chromaticity = converter.GetChromaticityOfCCT(5454); // x=0.33, y=0.34

double cct = converter.GetCCTOfChromaticity(new ChromaticityCoordinates(0.31271, 0.32902)); // cca 6500 K 
```

To obtain chromaticity of color in any color space, use conversion to **CIE xyY** color space. To obtain color from chromaticity (xy), just add the luminance **Y**. 

### Color difference

Colourful has several formulas for computing &#916;E (difference between colors). The usage is trivial:

```csharp
var color1 = new LabColor(l1, a1, b1);
var color2 = new LabColor(l2, a2, b2);

double deltaE = new CIEDE2000ColorDifference().ComputeDifference(color1, color2);
```

*For more details, see the detailed XML documentation, or the [unit tests](https://github.com/tompazourek/Colourful/tree/master/Colourful.Tests).*

What is supported
-----------------

### Color spaces

Colourful currently supports following color spaces (and conversions between each other):

* **RGB** *(see working spaces below)*
* **CIE 1931 XYZ**
* **CIE xyY** *(derived from XYZ)*
* **CIE L\*a\*b\* (1976)**
* **CIE L\*C\*h°<sub>ab</sub>**
* **CIE L\*u\*v\* (1976)**
* **Hunter Lab**

All of these color spaces (including RGB) have double precision. Conversion to `System.Drawing.Color`, which is 8-bit, is supported from `RGBColor` through implicit type-conversion operator to make integration seamless.

### RGB working spaces

  * sRGB
  * Simplified sRGB
  * ECI RGB v2
  * Adobe RGB (1998)
  * Apple sRGB
  * Best RGB
  * Beta RGB
  * Bruce RGB
  * CIE RGB
  * ColorMatch RGB
  * Don RGB 4
  * Ekta Space PS5
  * NTSC RGB
  * PAL/SECAM RGB
  * ProPhoto RGB
  * SMPTE-C RGB
  * Wide Gamut RGB
  * Rec. 709
  * Rec. 2020
  * *(custom RGB working spaces)*

### Illuminants &mdash; white points

* A
* B
* C
* D50
* D55
* D65
* D75
* E
* F2
* F7
* F11
* *(custom white points)*

### Chromatic adaptation methods

* Bradford
* Von Kries
* XYZ scaling
* *(custom chromatic adaptation)*

### Color difference formulas

* CIE Delta-E 1976
* CMC l:c (1984)
* CIE Delta-E 1994
* CIE Delta-E 2000
