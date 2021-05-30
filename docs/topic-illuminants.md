# Illuminants and white points

When doing [color conversion](topic-conversion.md) and other color manipulation, we're usually dealing with colors with relation to an illuminant. The illuminant is a theoretical description of a light source. As every light source, it has it's color/temperature, and is described by its white point. In layman's terms, the white point is similar to white balance on digital cameras.

Color spaces tend to use specific white points. For example, the [sRGB working space of the RGB color space](spaces-rgb.md) uses the white point of the CIE Standard Illuminant D65 which is intended to represent the average midday light in Europe. It has [color temperature](topic-cct.md) of about 6504 K, which makes it a little bit colder (bluish shade) color.

Other color spaces can use different white points. The *Wide Gamut RGB* color space, for example, uses a slightly warmer D50 illuminant. The same D50 is also often used in the [Lab color space](spaces-lab.md) used in the printing industry.

Because the white points are important to the color spaces, they sometimes need to be explicitly specified during [color conversion](topic-conversion.md). The Colourful library then automatically adjusts the colors during the conversion via a process called [chromatic adaptation](topic-conversion.md#chromatic-adaptation).


## Built-in illuminants

When white points are specified in Colourful, they are specified using the [XYZ color space](spaces-xyz.md). There are many illuminants already built into the library as constants. You can access them via the `Colourful.Illuminants` static class.

```csharp
var myWhite = Illuminants.D65; // XYZ [X=0.95047, Y=1, Z=1.08883]
```

The coefficients of the built-in illuminants were collected from this page: http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html


## Specifying custom illuminants

If you want, you can use your own white points as follows:

```csharp
var customWhite = new XYZColor(0.95041, 1, 1.08747);
```

As for RGB, there are [RGB working spaces](spaces-rgb.md) that each contain the reference white point used in that working space.


## Chromaticity and xy space

Colourful uses the [XYZ color space](spaces-xyz.md) to define the white points. But if you look at the data, you might notice that the Y luminance channel is always 1. The luminance of white point is always the maximum value, it's actually the color/chromaticity of the white point that is important.

Alternatively, you could specify your white points using just the [xy chromaticity space](spaces-xy.md) and convert them to XYZ.

```csharp
var converter = new ConverterBuilder().Fromxy().ToXYZ().Build();
var d93Chromaticity = new xyChromaticity(0.28315, 0.29711); // D93 used for high-efficiency blue phosphor monitors, BT.2035
var d93WhitePoint = converter.Convert(d93Chromaticity); // XYZ [X=0.95, Y=1, Z=1.41]
```


## Related links

- https://en.wikipedia.org/wiki/Standard_illuminant
- https://en.wikipedia.org/wiki/Illuminant_D65
- http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html