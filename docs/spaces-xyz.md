# XYZ color space

## XYZ

- **Type:** `XYZColor`
- **Channels:**
  - **X** from 0 to 1
  - **Y** (Luminance) from 0 to 1
  - **Z** from 0 to 1

The CIE 1931 XYZ color space is one of the basic color spaces in Colourful. It's utilized for many applications. The [white points of illuminants](topic-illuminants.md) are specified in XYZ. It's also used in majority of conversions between color spaces, as most color spaces define conversion to/from XYZ. So when we convert between two color spaces, it's very likely we'll need to go through the XYZ color space as intermediate.

```csharp
// red
var c1 = new XYZColor(0.3769, 0.2108, 0.0694);

// white (D65)
var c2 = new XYZColor(0.95047, 1, 1.08883);

// gray (D65)
var c3 = new XYZColor(0.2034, 0.2140, 0.2331);

// black
var c4 = new XYZColor(0, 0, 0);
```


## xy and xyY

The CIE XYZ has an analogue [xyY color space and xy chromaticity](spaces-xy.md). Follow the link to the dedicated documentation page about these.

**Note that the xy chromaticity isn't simply the X and Y channels of XYZ. Uppercase/lowercase X and Y mean something different.**


## Conversion strategy

Since XYZ is considered as the "base" space, it's actually the other color spaces define how to convert to XYZ.


## White points and chromatic adaptation

Most of the time when talking about colors, you need to specify the reference white point. For more information about this, see the [Illuminants and white points](topic-illuminants.md) page.


## How to convert between XYZ and RGB?

In this example, the conversion from [RGB color space](spaces-rgb.md) to XYZ is demonstrated. We use the sRGB working space and keep the [D65 white point](topic-illuminants.md) so no [chromatic adaptation](topic-conversion.md#chromatic-adaptation) is needed.

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToXyz = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToXYZ(rgbWorkingSpace.WhitePoint).Build();
var outputXyz = rgbToXyz.Convert(inputRgb); // XYZ [X=0.38, Y=0.21, Z=0.07]
```

The reverse is similar:

```csharp
var inputXyz = new XYZColor(0.3769, 0.2108, 0.0694);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var xyzToRgb = new ConverterBuilder().FromXYZ(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = xyzToRgb.Convert(inputXyz); // RGB [R=0.94, G=0.2, B=0.25]
```


## Related links

- https://en.wikipedia.org/wiki/CIE_1931_color_space