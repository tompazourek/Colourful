# RGB color spaces

## RGB

- **Type:** `RGBColor`
- **Channels:**
  - **R** (red) from 0 to 1
  - **G** (green) from 0 to 1
  - **B** (blue) from 0 to 1

RGB is probably the most commonly known color space. In computers, you might often see the three channels ranging from 0 to 255 (8 bits per channel, 24 bits total). However, in Colourful, **the RGB color channels range from 0 to 1.**

```csharp
// red
var c1 = new RGBColor(0.937, 0.2, 0.251);
var c2 = RGBColor.FromRGB8Bit(239, 51, 64);
var c3 = RGBColor.FromColor(System.Drawing.Color.FromArgb(239, 51, 64));

// gray
var c4 = new RGBColor(0.5, 0.5, 0.5);
var c5 = RGBColor.FromGray(0.5);

// white
var c6 = new RGBColor(1, 1, 1);

// black
var c7 = new RGBColor(0, 0, 0);
```


## RGB working spaces

It's important to realize that **there isn't only one RGB**. There are, in fact, multiple RGB color spaces. In Colourful, there is a single `RGBColor` type, and then during conversion, you can specify a so-called "RGB working space". In digital applications, the most commonly used on is the **sRGB** color space. However, there are more, e.g. *Adobe RGB (1998)*, *Apple RGB*, *NTSC RGB*, *Wide Gamut RGB*.

RGB working space consists of three ingredients:

- White point
- Companding function
- Chromaticity of the red, green, and blue primaries


### White point

Each RGB working space has a [reference white point](topic-illuminants.md). For example, *sRGB* has D65, the *Wide Gamut RGB* has slightly warmer D50, etc.

To get the white point of the working space:

```csharp
RGBWorkingSpace workingSpace = RGBWorkingSpaces.sRGB;
XYZColor whitePoint = workingSpace.WhitePoint; // corresponds to D65
```


### Companding function

A companding function (`ICompanding`) is used to convert between `RGBColor` and `LinearRGBColor` and back. This is a necessary part of the conversion process.

Many working spaces use the simple `GammaCompanding` function with a specified gamma. Some working spaces use a different specialized equation, e.g. `LCompanding`, `Rec2020Companding`, `Rec709Companding`, or `sRGBCompanding`.

If you want to implement a custom RGB working space, you can either pick one of the built-in companding functions, or implement the `ICompanding` interface.


### RGB primaries

The RGB working space has three so-called "primaries": red, green, and blue. Each of those primaries is defined by a [xy chromaticity](spaces-xy.md).

For example, the sRGB working space is defined as follows:

```csharp
var sRGB = new RGBWorkingSpace(
    Illuminants.D65,
    new sRGBCompanding(),
    new RGBPrimaries(
        new xyChromaticity(x: 0.6400, y: 0.3300),
        new xyChromaticity(x: 0.3000, y: 0.6000),
        new xyChromaticity(x: 0.1500, y: 0.0600)
    )
);
```

Where the `sRGBCompanding` looks as follows:

```csharp
/*...*/

public double ConvertToLinear(in double x)
    => x <= 0.04045 ? x / 12.92 : Math.Pow((x + 0.055) / 1.055, 2.4);

public double ConvertToNonLinear(in double x)
    => x <= 0.0031308 ? 12.92 * x : 1.055 * Math.Pow(x, 1 / 2.4d) - 0.055;

/*...*/
```


### Built-in working spaces

Colourful comes with many predefined RGB working spaces. To access those, see the `RGBWorkingSpaces` static class.

In most cases, you'll probably want to use `RGBWorkingSpaces.sRGB`, which is also the default if you don't specify any.


## Linear RGB

- **Type:** `LinearRGBColor`
- **Channels:**
  - **R** (red) from 0 to 1
  - **G** (green) from 0 to 1
  - **B** (blue) from 0 to 1

```csharp
// red
var c1 = new LinearRGBColor(0.863, 0.0331, 0.0513);

// gray
var c4 = new LinearRGBColor(0.5, 0.5, 0.5);
var c5 = LinearRGBColor.FromGray(0.5);

// white
var c6 = new LinearRGBColor(1, 1, 1);

// black
var c7 = new LinearRGBColor(0, 0, 0);
```

Linear RGB is almost the same color space as the RGB above. The only difference is that it was already adjusted via the companding function (see above).

Due to the linear intensity of the linear RGB space, it is more useful for any sort of blending/shading applications. That is because when you use basic mathematic operations like adding, subtracting, dividing, multiplying, averaging, etc., these work better if the intensity is linear.

**If you intend to do any sort of custom math operations with an RGB color, you should probably use the linear RGB color space.** Usually, you'd start with sRGB, then convert to linear RGB, do the computation, and then convert back.


### Example

In the following example, this approach is demonstrated via blending two colors:

- #FF0000 ![#FF0000](https://via.placeholder.com/10/FF0000/000000?text=+)
- #0000FF ![#0000FF](https://via.placeholder.com/10/0000FF/000000?text=+)

The blending is done simply by averaging the corresponding channels. 

If the blending would be done in `RGBColor`, the resulting color would be #800080 ![#800080](https://via.placeholder.com/10/800080/000000?text=+).

However, if we convert to `LinearRGB`, perform the computation there, and then convert out, we get #BC00BC ![#BC00BC](https://via.placeholder.com/10/BC00BC/000000?text=+), which more closely corresponds to the actual perceived average between the two colors.

```csharp
RGBColor color1 = RGBColor.FromRGB8Bit(255, 0, 0);
RGBColor color2 = RGBColor.FromRGB8Bit(0, 0, 255);

var rgbToLinear = new ConverterBuilder().FromRGB().ToLinearRGB().Build();

LinearRGBColor linear1 = rgbToLinear.Convert(color1);
LinearRGBColor linear2 = rgbToLinear.Convert(color2);

var linearBlend = new LinearRGBColor(
    (linear1.R + linear2.R) / 2,
    (linear1.G + linear2.G) / 2,
    (linear1.B + linear2.B) / 2
);

var linearToRgb = new ConverterBuilder().FromLinearRGB().ToRGB().Build();

RGBColor blend = linearToRgb.Convert(linearBlend);
blend.ToRGB8Bit(out var r, out var g, out var b); // (188, 0, 188) instead of (128, 0, 128)
```


## Utils

### Clamp and normalize intensity

If you are converting from a different color space with a wider/narrower gamut, or do any sort of other computation, you could end up with a value that's outside of the expected range from 0 to 1.

You can use the `.Clamp()` or the `.NormalizeIntensity()` utilities to make sure the values fall into the expected range. For more information, see the [dedicated page about this](topic-clamp.md).


### System.Drawing.Color conversions

In .NET, you might often come across the `System.Drawing.Color` type that also corresponds to RGB colors.

The `RGBColor` can integrate with `System.Drawing.Color` very well.

You can either use the `.FromColor()` or `.ToColor()` utilities, or alternatively, you can use the conversions.

The `RGBColor` to `System.Drawing.Color` type coercion is implicit, which means that if you have a function that accepts `System.Drawing.Color`, you can use `RGBColor` instead without any hassle.

The `System.Drawing.Color` to `RGBColor` type coercion is explicit, which means that if you have a function that accepts `RGBColor`, you can use a `System.Drawing.Color` value if you add the `(RGBColor)` explicit conversion.


### 8-bit (0 to 255) conversions

Because you'll often deal with RGB colors with the range of 0 to 255, there are two utilities to help you deal with this: `RGBColor.FromRGB8Bit(r, g, b)` and `.ToRGB8Bit(out var r, out var g, out var b)`.


### Gray helper

In case you're dealing with a gray value, you can use the `.FromGray()` utility that will set all three channels to the same value.

This utility is available for both `RGBColor` and `LinearRGBColor`.


## Conversion strategy

When converting between `RGBColor` and any other color space, it always goes through the `LinearRGBColor`.

When converting between `LinearRGBColor` and any other color space, it goes through the [XYZ color space](spaces-xyz.md).

**Note that if you don't specify an RGB working space, the `RGBWorkingSpaces.sRGB` will be used by default.**


## Related links

- https://stackoverflow.com/questions/12524623/what-are-the-practical-differences-when-working-with-colors-in-a-linear-vs-a-no
- https://en.wikipedia.org/wiki/RGB_color_space
- https://en.wikipedia.org/wiki/RGB_color_model
- http://www.brucelindbloom.com/index.html?WorkingSpaceInfo.html
- https://en.wikipedia.org/wiki/Gamma_correction
