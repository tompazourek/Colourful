# Cylindrical color spaces

Many of the color spaces also have their cylindrical equivalents (sometimes also called "polar" equivalents). A cylindrical color space usually consists of three channels:

- Lightness channel, often denoted as "L"
- Chroma (also chrominance or colourfulness) channel, often denoted as "C"
- Hue channel, often denoted as "h"

The lightness and chroma channels have different ranges depending on the color space (some use 0 to 100, some use 0 to 1, etc.)

The hue channel is specified in degrees, and has values from 0° to 360°.


## Color spaces with cylindrical equivalents

These are some of the color spaces that are built-in and have cylindrical equivalent:

- [Lab and corresponding LCHab](spaces-lab.md)
- [Luv and corresponding LCHuv](spaces-luv.md)
- [J<sub>z</sub>a<sub>z</sub>b<sub>z</sub> and corresponding J<sub>z</sub>C<sub>z</sub>h<sub>z</sub>](spaces-jzazbz.md)


## Helper formulas

If you want to experiment with cylindrical representations, you can explore the formulas available on the `CylindricalFormulas` static helper class.

It provides methods like:

- `GetSaturation` that computes saturation from chroma and lightness.
- `GetChroma` that computes chroma from saturation and lightness.
- `ConvertToLCh` that can convert a vector with lightness and two color channels into lightness, chroma, hue vector.
- `ConvertFromLCh` that can convert a vector with lightness, chroma, and hue into and a vector with lightness and two color channels.


## Related links

- https://en.wikipedia.org/wiki/Chrominance
- https://en.wikipedia.org/wiki/Lightness
- https://en.wikipedia.org/wiki/Luma_(video)
- https://en.wikipedia.org/wiki/List_of_color_spaces_and_their_uses#Cylindrical_transformations
