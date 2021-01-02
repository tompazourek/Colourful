# Correlated color temperature (CCT)

Colourful supports working with **correlated color temperature (CCT)** in two ways:

- Approximating the [xy chromaticity](spaces-xy.md) from a CCT specified in K.
- Approximating the CCT from a given [xy chromaticity](spaces-xy.md).


## How to compute color from CCT?

Your input in this case is a temperature in kelvin (K) and you want to figure out what color this corresponds to.

Colourful provides a helper class `CCTConverter` that can be used to approximate the [xy chromaticity](spaces-xy.md) from the given temperature.

```csharp
double temperature = 3000; // in K
xyChromaticity chromaticity = CCTConverter.GetChromaticityOfCCT(temperature); // xy [x=0.44, y=0.4]
```

Using this you'll only have the chromaticity, not an actual color. However, you can use a [color converter](topic-conversion.md) to convert this to the target color space. In the example below, we'll try to represent the color in the sRGB color space.

```csharp
IColorConverter<xyChromaticity, RGBColor> converter = new ConverterBuilder().Fromxy(Illuminants.D65).ToRGB(RGBWorkingSpaces.sRGB).Build();
RGBColor color = converter.Convert(chromaticity); // RGB [R=1.28, G=0.93, B=0.56]
```

You'll notice that the resulting [RGB color](spaces-rgb.md) is outside of its range between 0 and 1, the red channel exceeds this. This is because the input chromaticity doesn't have a luminance associated with it, and during the color conversion, it used the maximum luminance of 1 in the [xyY color space](spaces-xy.md). And the resulting color is outside of the range. To get a value inside the RGB range, we can divide the individual channel by the highest channel value (in this case it's 1.28). This will make at least one of the channels with the maximum value 1, and still keep the right hue. In Colourful, there's a helper for this for RGB colors called `NormalizeIntensity`.

```csharp
color = color.NormalizeIntensity(); // RGB [R=1, G=0.72, B=0.44]
```

This value corresponds to RGB values 255, 185, 111, or ![#FFB96F](https://via.placeholder.com/15/FFB96F/000000?text=+) `#FFB96F` in the hex format.



### Planckian locus approximation

To approximate chromaticity from CCT, Colourful currently uses the Planckian locus approximation method. The algorithm is described [here](https://en.wikipedia.org/wiki/Planckian_locus#Approximation).

Alternative methods aren't yet implemented (see [#74](https://github.com/tompazourek/Colourful/issues/74)).


## How to compute temperature of a color?

Your input in this case is a color and you want to approximate what correlated color temperature (CCT) in kelvin (K) it corresponds to.

```csharp
RGBColor color = RGBColor.FromRGB8Bit(255, 121, 0);
IColorConverter<RGBColor, xyChromaticity> converter = new ConverterBuilder().FromRGB(RGBWorkingSpaces.sRGB).Toxy(Illuminants.D65).Build();
xyChromaticity chromaticity = converter.Convert(color); // xy [x=0.55, y=0.4]
double temperature = CCTConverter.GetCCTOfChromaticity(chromaticity); // 1293 K
```

The results are unfortunately not too precise and sometimes you might end up with values outside of the expected range.


### Low-temperature CCT approximation equation

To approximate CCT from chromaticity, Colourful currently uses low-temperature equation proposed by J. Hernández-Andrés, R. L. Lee, and J. Romero. The algorithm is described [here](https://en.wikipedia.org/wiki/Color_temperature#Approximation).

Alternative methods aren't yet implemented (see [#74](https://github.com/tompazourek/Colourful/issues/74)).


## Related links

- https://en.wikipedia.org/wiki/Color_temperature#Correlated_color_temperature
- https://en.wikipedia.org/wiki/Planckian_locus#Approximation
- https://en.wikipedia.org/wiki/Color_temperature#Approximation
