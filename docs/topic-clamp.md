# Ranges of channel values and clamping

Coming in the [release of v3, one of the major changes](topic-changes-v2-v3.md) is that the channel values can land outside of the color space. The [RGB color space](spaces-rgb.md), for example, is represented by three channels ranging from 0 to 1. But when converting from a color space that has a wider color gamut, it's possible to end up with RGB color where the values are either below 0 or above 1.


## Clamping

There's a utility class `ClampHelper` that can help you crop the values. You can use it both on a single value, or on an array. It also works as an extension method.

```csharp
(1.25).Clamp(0, 1); // 1
(-0.5).Clamp(0, 1); // 0
(0.75).Clamp(0, 1); // 0.75
```

For vectors:

```csharp
new double[] { 1.25, -0.5, 0.75 }.Clamp(0, 1); // { 1, 0, 0.75 }
```

For vectors with an additional vectors for minima and maxima:

```csharp
new double[] { -50, 75, 1.25 }.Clamp(new double [] { 0, 0, 0 }, new double [] { 100, 100, 1 }); // { 0, 75, 1 }
```

Some color spaces, like the RGB color space, might also have the `Clamp()` utility directly on the color:

```csharp
RGBColor color = new RGBColor(2, -3, 0.5);
var clampedColor = color.Clamp(); // RGB [R=1, G=0, B=0.5]
```


## Normalizing intensity

An alternative way of handling channel values outside of their expected range is to normalize their values.

One of the ways to normalize the values is to find the largest channel value, and then divide all channels by this largest values.

For RGB, there's a helper called `NormalizeIntensity`:

```csharp
RGBColor color = new RGBColor(2, -3, 0.5);
var normalizedColor = color.NormalizeIntensity(); // RGB [R=1, G=0, B=0.25]
```

**Note that in most cases, you should use the `NormalizeIntensity` in the linear RGB color space `LinearRGBColor`.** This means to convert to linear RGB, do the normalization there, and then convert to the final RGB. See the [RGB color space page](spaces-rgb.md) for more information.

```csharp
LinearRGBColor color = new LinearRGBColor(2, -3, 0.5);
var normalizedColor = color.NormalizeIntensity(); // LinearRGBColor [R=1, G=0, B=0.25]
```