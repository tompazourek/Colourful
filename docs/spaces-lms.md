# LMS color space

## LMS

- **Type:** `LMSColor`
- **Channels:**
  - **L** (long) usually between 0 and 1
  - **M** (medium) usually between 0 and 1
  - **S** (short) usually between 0 and 1

The LMS color space is designed to represent the response of three types of cones in the human eye:

- Long cones (L) that respond to long wavelengths (red light).
- Medium cones (M) that respond to medium wavelengths (green light).
- Short cones (S) that respond to short wavelengths (blue light).

LMS is used in Colourful to perform [chromatic adaptation](topic-conversion.md#chromatic-adaptation) either during conversion between color spaces that use different white points, or alternatively to [white-balance](topic-conversion.md#white-balance) a color.

```csharp
// red
var c1 = new LMSColor(0.3823, 0.0811, 0.07162);

// white (D65)
var c2 = new LMSColor(0.9414, 1.0404, 1.0895);

// gray (D65)
var c3 = new LMSColor(0.2015, 0.2227, 0.2332);

// black
var c4 = new LMSColor(0, 0, 0);
```


## Conversion strategy

When we convert from `LMSColor` to any other color space, the conversion always goes through the [XYZ color space](spaces-xyz.md). The `LMSColor` can also appear in the conversion process as chromatic adaptation is performed in this color space.

The `ConverterBuilder` class can be set up with different LMS transformation matrices that correspond to different methods of chromatic adaptation. See more at [LMS transformation matrix](topic-conversion.md#lms-transformation-matrix).


## Convert between LMS and RGB

In this example, the conversion from [RGB color space](spaces-rgb.md) to LMS is demonstrated. We use the sRGB working space and keep the [D65 white point](topic-illuminants.md).

```csharp
var inputRgb = new RGBColor(0.937, 0.2, 0.251);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var rgbToLms = new ConverterBuilder().FromRGB(rgbWorkingSpace).ToLMS(rgbWorkingSpace.WhitePoint).Build();
var outputLms = rgbToLms.Convert(inputRgb); // LMS [L=0.38, M=0.08, S=0.07]
```

The reverse is similar:

```csharp
var inputLms = new LMSColor(0.3823, 0.0811, 0.07162);
var rgbWorkingSpace = RGBWorkingSpaces.sRGB;

var lmsToRgb = new ConverterBuilder().FromLMS(rgbWorkingSpace.WhitePoint).ToRGB(rgbWorkingSpace).Build();
var outputRgb = lmsToRgb.Convert(inputLms); // RGB [R=0.94, G=0.2, B=0.25]
```


## Related links

- https://en.wikipedia.org/wiki/LMS_color_space
- http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
