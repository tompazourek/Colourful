# Changes between v2 and v3

The version 3 of Colourful comes with **significant breaking changes** that will require you to update your code if you intend to update from version 2.


## New color conversion engine

In v2, the way to convert between color spaces was via the single `ColourfulConverter` that could be configured via its properties.

In v3, the conversion engine was rebuilt from scratch. This is mainly to support the exensibility aspect, to be able to add many more color spaces in the future and allow conversion between all of them. This also allows consumers to implement their own color space.

**If you used the `ColourfulConverter`, you'll need to rewrite your code.** For more information on how to do that, see the [Conversion between color spaces
](topic-conversion.md) page.


## Channel range is not clamped by default

A major change is that the ranges of channel values are not automatically checked or clamped. For example, the typical range of channel values in the [RGB color space](spaces-rgb.md) is from 0 to 1. But now, you can end up with values outside of this space.

This is done mainly for these reasons:

- So we wouldn't lose the color information in case we're doing some processing and then converting the color to another color space.
- Because in different scenarios you might want to treat the outside values differently. For example, sometimes you might want to simply clamp them (i.e. if it's higher than 1, clamp it to 1, etc.), but in other scenarios, you might want to normalize the values (e.g. by finding the highest channel value, and then divide all channels by it, for RGB, see the `.NormalizeIntensity()` function).

For more information about this, see the [Ranges of channel values and clamping](topic-clamp.md) and [Conversion between color spaces
](topic-conversion.md) pages.


## New J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> color space

There's now a new [J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> color space](spaces-jzazbz.md) with its [cylindrical equivalent J<sub>z</sub>C<sub>z</sub>h<sub>z</sub>](topic-cylindrical-spaces.md) and new [ΔE<sub>z</sub> color difference algorithm](topic-color-difference.md).


## Deconstructors

All colors can now be deconstructed as follows:

```csharp
var myColor = new RGBColor(0.2, 0.205, 0.45445);

// ...

var (r2, g2, b2) = myColor; // deconstruct into three variables
```

This is utilizing the [tuple deconstruct feature from C# 7.0](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/deconstruct).


## Increased test coverage

Lots of automated tests have been added to increase the test coverage of the existing code.


## New documentation

As part of the v3 release, there was also a bigger focus on documentation. As a result of that, there are now more detailed documentation topics. See the [documentation home page](README.md) for links.


## Many new helper classes

There are now lots of new helpers, for example:

- `ClampHelper` for [clamping channel values](topic-clamp.md)
- `CylindricalFormulas` for [cylindrical color spaces](topic-cylindrical-spaces.md)
- `.NormalizeIntensity()` helper for [RGB colors](spaces-rgb.md)
