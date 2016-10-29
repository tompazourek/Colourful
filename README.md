![Colourful logo](https://raw.githubusercontent.com/tompazourek/Colourful/master/assets/logo_32.png) Colourful .NET
==============

[![Build status](https://ci.appveyor.com/api/projects/status/xegjq1k7ixfrf157)](https://ci.appveyor.com/project/tompazourek/colourful)

*Open source .NET library for working with color spaces.*

The library is written in C# and released with an [MIT license](https://raw.githubusercontent.com/tompazourek/Colourful/master/LICENSE), so feel **free to fork** or **use commercially**.

**Any feedback is appreciated, please visit the [issues](https://github.com/tompazourek/Colourful/issues?state=open) page or send me an [e-mail](mailto:tom.pazourek@gmail.com).**

Download
--------

Binaries of the last build can be downloaded on the [AppVeyor CI page of the project](https://ci.appveyor.com/project/tompazourek/colourful/build/artifacts).

The library is also [published on NuGet.org](https://www.nuget.org/packages/Colourful/), install using:

```
PM> Install-Package Colourful
```

<sup>Colourful is CLS Compliant (to allow use in VB.NET etc.) and is built as for .NET v4.6, .NET v4.5, v4.0, v3.5, .NET Standard 1.1 and .NET Standard 1.6.</sup>


Usage
-----

### Color conversion

```csharp
RGBColor input = new RGBColor(1, 0, 0);

var converter = new ColourfulConverter { WhitePoint = Illuminants.D65 };

XYZColor output = converter.ToXYZ(input);
```

The `ColourfulConverter` facade can convert **from any of the [supported color spaces](#color-spaces) to any other color space**.

It **always performs the chromatic adaptation** if the input and output color space white points are different.

### Chromatic adaptation

The adaptation can be also performed alone (e.g. from CIELAB D50 to CIELAB D65).


```csharp
LabColor input = new LabColor(10, 20, 30, Illuminants.D50);

var converter = new ColourfulConverter { TargetLabWhitePoint = Illuminants.D65 };

LabColor output = converter.Adapt(input);
```

### Conversion between RGB working spaces

Adaptation can also convert from one RGB working space to another (e.g. from sRGB to Adobe RGB).

```csharp
RGBColor input = new RGBColor(Color.Yellow, RGBWorkingSpaces.sRGB);

var converter = new ColourfulConverter { TargetRGBWorkingSpace = RGBWorkingSpaces.AdobeRGB1998 };

RGBColor output = converter.Adapt(input);
```

Converter can be configured to arbitrary chromatic adaptation method, [several are supported](#chromatic-adaptation-methods).

### CCT approximation

Colourful also supports computing **correlated color temperature (CCT)** from chromaticity and computing chromaticity from CCT. Although these are just approximations with low precision.

```csharp
var converter = new CCTConverter();

ChromaticityCoordinates chromaticity = converter.GetChromaticityOfCCT(5454); // x=0.33, y=0.34

double cct = converter.GetCCTOfChromaticity(new ChromaticityCoordinates(0.31271, 0.32902)); // cca 6500 K 
```

To obtain chromaticity of a color in any color space, use conversion to **CIE xyY** color space. To obtain color from chromaticity (xy), just add the luminance **Y** and the result is **xyY**. 

### Color difference

Colourful has several formulas for computing &#916;E (difference between colors). The usage is trivial:

```csharp
var color1 = new LabColor(l1, a1, b1);
var color2 = new LabColor(l2, a2, b2);

double deltaE = new CIEDE2000ColorDifference().ComputeDifference(color1, color2);
```

*For more details, see the detailed XML documentation (generated during build), or the [unit tests](https://github.com/tompazourek/Colourful/tree/master/Colourful.Tests).*

### Macbeth ColorChecker

Colourful contains definition of all 24 [Macbeth ColorChecker](http://en.wikipedia.org/wiki/ColorChecker) colors in sRGB, which are useful for color calibration. Access them this way:

```csharp
RGBColor[] testColors = MacbethColorChecker.Colors; // array of 24 colors

RGBColor testColor = MacbethColorChecker.DarkSkin; // individual colors
```


What is supported
-----------------

### Color spaces

Colourful currently supports following color spaces (and conversions between each other):

* **RGB** *(see working spaces below)*
* **linear RGB** <sup>*</sup>
* **CIE XYZ (1931)**
* **CIE xyY** *(derived from XYZ)*
* **CIE L\*a\*b\* (1976)** *(CIELAB)*
* **CIE L\*C\*h°<sub>ab</sub>** *(CIELCH)*
* **CIE L\*u\*v\* (1976)** *(CIELUV)*
* **CIE L\*C\*h°<sub>uv</sub>** *(CIELCH)*
* **Hunter Lab**
* **LMS** *(cone response)*

<sup>* for more information, see: http://stackoverflow.com/questions/12524623/what-are-the-practical-differences-when-working-with-colors-in-a-linear-vs-a-no</sup>

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
  * Rec. 709 *(ITU-R Recommendation BT.709 &ndash; HDTV)*
  * Rec. 2020 *(ITU-R Recommendation BT.2020 &ndash; UHDTV)*
  * *(custom RGB working spaces)*

### Illuminants &mdash; white points

* A *(Incandescent / Tungsten)*
* B *(Direct sunlight at noon (obsolete))*
* C *(Average / North sky Daylight (obsolete))*
* D50 *(Horizon Light. ICC profile PCS)*
* D55 *(Mid-morning / Mid-afternoon Daylight)*
* D65 *(Noon Daylight: Television, sRGB color space)*
* D75 *(North sky Daylight)*
* E *(Equal energy)*
* F2 *(Cool White Fluorescent)*
* F7 *(D65 simulator, Daylight simulator)*
* F11 *(Philips TL84, Ultralume 40)*
* *(custom white points)*

### Chromatic adaptation

Right now there is only one chromatic adaptation method, the **von Kries chromatic adaptation method**. Custom used-defined chromatic adaptation methods are also supported. The von Kries chromatic adaptation method can be parametrized using **LMS transformation matrix**. These LMS transformation matrices are available:

* Bradford (default)
* Von Kries (Hunt-Pointer-Estevez adjusted for D65)
* Von Kries (Hunt-Pointer-Estevez for equal energy)
* XYZ scaling
* Spectral-sharpened Bradford 
* CMCCAT2000
* CAT02
* *(custom chromatic adaptation)*

### Color difference formulas (ΔE)

* CIE Delta-E 1976
* CMC l:c (1984)
* CIE Delta-E 1994
* CIE Delta-E 2000
