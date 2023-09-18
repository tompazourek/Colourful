# ![Colourful logo](https://raw.githubusercontent.com/tompazourek/Colourful/master/assets/logo_32.png) Colourful .NET

[![Build status](https://img.shields.io/appveyor/ci/tompazourek/colourful/master.svg)](https://ci.appveyor.com/project/tompazourek/colourful)
[![Tests](https://img.shields.io/appveyor/tests/tompazourek/colourful/master.svg)](https://ci.appveyor.com/project/tompazourek/colourful/build/tests)
[![codecov](https://codecov.io/gh/tompazourek/Colourful/branch/master/graph/badge.svg?token=gSGKtsdmw3)](https://codecov.io/gh/tompazourek/Colourful)
[![NuGet version](https://img.shields.io/nuget/v/Colourful.svg)](https://www.nuget.org/packages/Colourful/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Colourful.svg)](https://www.nuget.org/packages/Colourful/)
[![API documentation](https://img.shields.io/badge/API%20Documentation-RobiniaDocs-43bc00?logo=readme&logoColor=white)](https://www.robiniadocs.com/d/colourful/api/Colourful.RGBColor.html)

*Open source .NET library for working with color spaces.*

The library is written in C# and released with an [MIT license](https://raw.githubusercontent.com/tompazourek/Colourful/LICENSE), so feel **free to fork** or **use commercially**.

**Any feedback is appreciated, please visit the [issues](https://github.com/tompazourek/Colourful/issues?state=open) page or send me an [e-mail](mailto:tom.pazourek@gmail.com).**


## Download

Binaries of the last build can be downloaded on the [AppVeyor CI page of the project](https://ci.appveyor.com/project/tompazourek/colourful/build/artifacts).

The library is also [published on NuGet.org](https://www.nuget.org/packages/Colourful/), install using:

```
PM> Install-Package Colourful
```

Colourful is CLS Compliant (to allow use in VB.NET etc.) and is built for these target frameworks:

- .NET 6
- .NET Framework 4.5
- .NET Standard 2.0
- .NET Standard 1.1
- *For older .NET Framework 4.0 see [version 1 of the library](https://github.com/tompazourek/Colourful/releases/tag/1.2.2).*


## Usage

Example "hello world" usage that converts a color from sRGB to XYZ (keeping the D65 white point):

```csharp
IColorConverter<RGBColor, XYZColor> converter = new ConverterBuilder()
    .FromRGB(RGBWorkingSpaces.sRGB)
    .ToXYZ(Illuminants.D65)
    .Build();

RGBColor rgbColor = new RGBColor(1, 0, 0.5);
XYZColor xyzColor = converter.Convert(rgbColor); // XYZ [X=0.45, Y=0.23, Z=0.22]
```


## Documentation

Please see the docs pages below for various topics:

- [Conversion between color spaces](docs/topic-conversion.md)
  - also handles chromatic adaptation with multiple possible LMS transformation matrices:
    - Bradford (default)
    - Von Kries (Hunt-Pointer-Estevez adjusted for D65)
    - Von Kries (Hunt-Pointer-Estevez for equal energy)
    - XYZ scaling
    - Spectral-sharpened Bradford 
    - CMCCAT2000
    - CAT02
    - *(user-defined chromatic adaptation matrix)*
- [Correlated color temperature (CCT)](docs/topic-cct.md)
  - Planckian locus approximation method
- [Ranges of channel values and clamping](docs/topic-clamp.md)
- [Computing color difference](docs/topic-color-difference.md)
  - multiple algorithms supported:
    - CIE Delta-E 1976
    - CMC l:c (1984)
    - CIE Delta-E 1994
    - CIE Delta-E 2000
    - J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> Delta-E<sub>z</sub>
    - Euclidean distance
- [Cylindrical color spaces](docs/topic-cylindrical-spaces.md)
- [Illuminants and white points](docs/topic-illuminants.md)
  - white points are handled correctly throughout the conversions
  - multiple illuminant are built-in:
    - A *(Incandescent / Tungsten)*
    - B *(Direct sunlight at noon (obsolete))*
    - C *(Average / North sky Daylight (obsolete))*
    - D50 *(Horizon Light. ICC profile PCS)*
    - D55 *(Mid-morning / Mid-afternoon Daylight)*
    - D65 *(Noon Daylight: Television, sRGB color space)*
    - D75 *(North sky Daylight)*
    - E *(Equal energy)*
    - F2 *(Cool White Fluorescent)*
    - F7 *(D65 simulator, Daylight simulator)*
    - F11 *(Philips TL84, Ultralume 40)*
    - *(user-defined white points)*
- [Macbeth ColorChecker chart](docs/topic-macbeth-color-checker.md)
- [Changes between v2 and v3](docs/topic-changes-v2-v3.md)

For information about specific color spaces, see the following docs pages:

- [RGB color spaces](docs/spaces-rgb.md)
  - support for both ordinary RGB and [linear RGB](http://stackoverflow.com/questions/12524623/what-are-the-practical-differences-when-working-with-colors-in-a-linear-vs-a-no)
  - multiple working spaces supported:
    - sRGB
    - Simplified sRGB
    - ECI RGB v2
    - Adobe RGB (1998)
    - Apple sRGB
    - Best RGB
    - Beta RGB
    - Bruce RGB
    - CIE RGB
    - ColorMatch RGB
    - Don RGB 4
    - Ekta Space PS5
    - NTSC RGB
    - PAL/SECAM RGB
    - ProPhoto RGB
    - SMPTE-C RGB
    - Wide Gamut RGB
    - Rec. 709 *(ITU-R Recommendation BT.709 &ndash; HDTV)*
    - Rec. 2020 *(ITU-R Recommendation BT.2020 &ndash; UHDTV)*
    - *(user-defined RGB working spaces)*
- [Lab color spaces](docs/spaces-lab.md)
  - CIE L\*a\*b\* (1976) *(CIELAB)*
  - CIE L\*C\*h°<sub>ab</sub> *(CIELCH)*
  - Hunter Lab
- [Luv color spaces](docs/spaces-luv.md)
  - CIE L\*u\*v\* (1976) *(CIELUV)*
  - CIE L\*C\*h°<sub>uv</sub> *(CIELCH)*
- [XYZ color space](docs/spaces-xyz.md)
  - CIE XYZ (1931)
  - CIE xyY *(derived from XYZ)*
- [J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> color spaces](docs/spaces-jzazbz.md)
  - J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> *(Safdar & al., 2017)*
  - J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> *(polar of J<sub>z</sub>a<sub>z</sub>b<sub>z</sub>)*
- [LMS color space](docs/spaces-lms.md)
- [xy chromaticity](docs/spaces-xy.md)
