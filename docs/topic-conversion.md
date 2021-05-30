# Conversion between color spaces

The main functionality facilitated by the Colourful library is the conversion between color spaces.

Colourful supports a range of color spaces out of the box. In addition to that, it's possible to [implement your own color space](topic-custom-color-space.md).


## Converter builder

Since version 3 of the library, you need to build a converter object if you want to perform any sort of conversion. The converter's job is to convert colors from a single source color space (noted as "From") to a single target color space (noted as "To").

To create a converter, you use a thing called **converter builder**, i.e. an instance of the `ConverterBuilder` class.

The `ConverterBuilder` has a fluent interface which works in several stages:

- Specify the source "From" color space, e.g. `.FromRGB()`.
- Specify the target "To" color space, e.g. `.ToXYZ()`.
- Call `.Build()` that produces the converter object.


### Example

For example, if you wish to build a converter from [RGB](spaces-rgb.md) to [XYZ color space](spaces-xyz.md), you'd build it using the builder as:

```csharp
var rgbToXyz = new ConverterBuilder().FromRGB().ToXYZ().Build();
```

If you need to convert in the opposite direction (from XYZ color space to RGB), you need another converter (remember converter always has a single source space and single target space):

```csharp
var xyzToRgb = new ConverterBuilder().FromXYZ().ToRGB().Build();
```

However, if you try to use these examples, you'll likely receive a `MissingConversionMetadataException` saying `"White point is not specified, but is required for the conversion."`

This indicates that for this conversion, a white point parameter (in the XYZ color space in this case) is necessary. See the section below for explanation.


## Conversion metadata

In addition to specifying the source and target color spaces, you can also specify additional conversion metadata. The metadata is related to either the source or target color spaces and can further specify the conversion parameters (e.g. which [RGB working space](spaces-rgb.md) we use, or what [white point](topic-illuminants.md) we use in each end).

The metadata is specified in the fluent interface converter builder as arguments to the `.FromXXX(...)` and `.ToXXX(...)` extension methods.


### Example

For example, the RGB color space has one additional parameter, which is the [RGB working space](spaces-rgb.md) (by default, it uses sRGB).

On the other end, in the XYZ color space, we also have one parameter, which is the white point used in the XYZ color space. For this one there is no default value.

```csharp
// assumes the source color is sRGB (which has the D65 white point), but we don't know the target white point, so we get MissingConversionMetadataException
var converter1 = new ConverterBuilder().FromRGB().ToXYZ().Build();

// assumes the source color is sRGB (which has the D65 white point), target will be XYZ with the D65 white point, same as sRGB, so we won't need to perform chromatic adaptation
var converter2 = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D65).Build();

// assumes the source color is sRGB (which has the D65 white point), target will be XYZ with the D50 white point, chromatic adaptation from D65 to D50 will be performed during the conversion 
var converter3 = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D50).Build();

// assumes the source color is ProPhoto RGB (which has D50 white point), target will be XYZ with the D50 white point, because both white points are the same, no chromatic adaptation will be performed
var converter4 = new ConverterBuilder().FromRGB(RGBWorkingSpaces.ProPhotoRGB).ToXYZ(Illuminants.D50).Build();
```

Note that the white points might not always be necessary. For example, when converting from [xy chromaticity space](spaces-xy.md) to [XYZ color space](spaces-xyz.md), the reference white points are optional, but you'd probably not usually specify them.

```csharp
// note no reference white points are needed here (they'd only be specified if they are different)
var converter5 = new ConverterBuilder().Fromxy().ToXYZ().Build();
```


### What conversion metadata are available?

Other color spaces might have different metadata that might be useful or even required to configure for some kinds of conversions. To see what conversion metadata is available, always check the parameters of the `.FromXXX(...)` and `.ToXXX(...)` methods to see what you can configure.

For most color spaces, you'll see a white point setting.


## Converter object

The type of the converter you build will be `IColorConverter<TSource, TTarget>`, e.g. `IColorConverter<RGBColor, XYZColor>` in our first example.

The `IColorConverter` interface is very simple, it only has one method:

```csharp
TTarget Convert(in TSource sourceColor);
```

The converter is intended to be built once, and then re-used for each conversion you need to do.

```csharp
// create the converter once (e.g. store it in a field somewhere)
private readonly IColorConverter<RGBColor, XYZColor> _rgbToXyz = new ConverterBuilder().FromRGB().ToXYZ(Illuminants.D65).Build();

// use it for conversion
var rgbColor1 = new RGBColor(1, 0, 0.5);
var xyzColor1 = _rgbToXyz.Convert(rgbColor1);

// not creating another converter, re-using the same
var rgbColor2 = new RGBColor(0.7, 0.2, 1);
var xyzColor2 = _rgbToXyz.Convert(rgbColor2);
```


## Range of values and clamping

There is one important [change for users coming from v2](topic-changes-v2-v3.md). During the conversion, the target values might actually end up outside of the range of the target color space.

For example, in RGB, we are used to work with channel values from 0 to 1 (correspond to 0 to 100% of red/green/blue). But if we're converting from a color space with a wider gamut than RGB, we might end up with values either below 0 or above 1. These values are NOT automatically clamped to fit the range.

This is done mainly for these reasons:

- So we wouldn't lose the color information in case we're doing some processing and then converting the color to another color space.
- Because in different scenarios you might want to treat the outside values differently. For example, sometimes you might want to simply clamp them (i.e. if it's higher than 1, clamp it to 1, etc.), but in other scenarios, you might want to normalize the values (e.g. by finding the highest channel value, and then divide all channels by it, for RGB, see the `.NormalizeIntensity()` function).

This additional processing is now left to the user. For more information about channel ranges, clamping, and the helpers that Colourful provides, see the [Ranges of channel values and clamping](topic-clamp.md) page.


## Chromatic adaptation

In case you want to perform [chromatic adaptation](https://en.wikipedia.org/wiki/Chromatic_adaptation), you can also use the converter builder to accomplish this. This is because **the source and target color spaces can actually be the same space**. That is, they'd only differ in their parameters.

For example, you might:

- Convert between [XYZ](spaces-xyz.md) relative to D50 to [XYZ](spaces-xyz.md) relative to D65.
- Convert between [Lab](spaces-lab.md) relative to D50 to [Lab](spaces-lab.md) relative to D65.
- Convert between [RGB](spaces-rgb.md) in sRGB working space to [RGB](spaces-rgb.md) in Adobe RGB working space.
- etc.


### Example

```csharp
var xyzD50ToD65 = new ConverterBuilder().FromXYZ(Illuminants.D50).ToXYZ(Illuminants.D65).Build();
var xyzAdapted = xyzD50ToD65.Convert(new XYZColor(0.5, 0.5, 0.5));

var labD50ToD65 = new ConverterBuilder().FromLab(Illuminants.D50).ToLab(Illuminants.D65).Build();
var labAdapted = labD50ToD65.Convert(new LabColor(50, -30, 75));

var sRgbToAdobe = new ConverterBuilder().FromRGB(RGBWorkingSpaces.sRGB).ToRGB(RGBWorkingSpaces.AdobeRGB1998).Build();
var rgbAdapted = sRgbToAdobe.Convert(new RGBColor(0.25, 0.65, 0.1));
```


### White balance

You can also create your own custom illuminant and use it to "white balance" the color to the desired illuminant.

```csharp
// adapt a XYZ color relative to D65 to a custom white point
var xyzCustomWhitePoint = new XYZColor(0.95, 0.54, 0.72);
var xyzCustomWhitePointAdapter = new ConverterBuilder().FromXYZ(Illuminants.D65).ToXYZ(xyzCustomWhitePoint).Build();
var xyzAdapted = xyzCustomWhitePointAdapter.Convert(new XYZColor(0.5, 0.5, 0.5));
```


### LMS transformation matrix

Chromatic adaptation between different white points is implemented by converting to the [LMS color space](spaces-lms.md) where the adaptation is performed.

The conversion to LMS is implemented via a matrix that's applied on a vector in the [XYZ color space](spaces-xyz.md). There are many different matrices that can be used to facilitate this conversion. By default, the Colourful library will use the Bradford matrix:

```csharp
public static readonly double[,] Bradford =
{
    { 0.8951, 0.2664, -0.1614 },
    { -0.7502, 1.7135, 0.0367 },
    { 0.0389, -0.0685, 1.0296 },
};
```

You can setup different LMS transformation matrices that are built into Colourful, or alternatively, you can also plug your own in.

One way to set up this LMS transformation matrix is via a constructor parameter of the `ConverterBuilder` class.

```csharp
// Default; Bradford chromatic adaptation transform matrix (used in CMCCAT97)
new ConverterBuilder();

// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez adjusted for D65).
new ConverterBuilder(LMSTransformationMatrix.VonKriesHPEAdjusted);

// Von Kries chromatic adaptation transform matrix (Hunt-Pointer-Estevez for equal energy).
new ConverterBuilder(LMSTransformationMatrix.VonKriesHPE);

// XYZ scaling chromatic adaptation transform matrix.
new ConverterBuilder(LMSTransformationMatrix.XYZScaling);

// Spectral sharpening and the Bradford transform.
new ConverterBuilder(LMSTransformationMatrix.BradfordSharp);

// CMCCAT2000 (fitted from all available color data sets).
new ConverterBuilder(LMSTransformationMatrix.CMCCAT2000);

// CAT02 (optimized for minimizing CIELAB differences).
new ConverterBuilder(LMSTransformationMatrix.CAT02);

// custom LMS transformation matrix
double[,] myCustomMatrix =
{
    { 1.2694, -0.0988, -0.1706 },
    { -0.8364, 1.8006, 0.0357 },
    { 0.0297, -0.0315, 1.0018 },
};
new ConverterBuilder(myCustomMatrix);
```

Note that **if no chromatic adaptation is performed during the conversion, then the chosen matrix has no effect**.


## Conversion strategies

What you might see as a user of the `ConverterBuilder` class is a mention of conversion strategies. This is an advanced concept used if you want to extend Colourful with your own custom color spaces, or if you want to further customize the conversion process. This might not be useful for most cases.

Implenting custom color spaces is possible, but not yet documented. See [#93](https://github.com/tompazourek/Colourful/issues/93).


## Related links

- https://en.wikipedia.org/wiki/Chromatic_adaptation
- http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
- [Illuminants and white points](topic-illuminants.md)
- [Ranges of channel values and clamping](topic-clamp.md)
- [Changes between v2 and v3](topic-changes-v2-v3.md)
