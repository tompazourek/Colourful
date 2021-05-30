# Computing color difference

The Colourful library contains a range of algorithms that can be used to compute a [difference between colors](https://en.wikipedia.org/wiki/Color_difference), i.e. provide a certain metric that can be used as a distance between two colors in a color space.

The color difference is commonly denoted as delta E, or ΔE. Some of the algorithms operate on specific color spaces. If you want to use them in other color spaces, you can do so if you [convert your colors](topic-conversion.md) into the required space beforehand.


## CIE Delta-E 1976

The CIE76 algorithm operates in the [Lab color space](spaces-lab.md).

```csharp
var labColor1 = new LabColor(55, 80, 50);
var labColor2 = new LabColor(18, 36, -60);
double difference = new CIE76ColorDifference().ComputeDifference(labColor1, labColor2); // 124.1169
```

### Related links

- http://www.brucelindbloom.com/index.html?Eqn_DeltaE_CIE76.html
- https://en.wikipedia.org/wiki/Color_difference#CIE76


## CMC l:c (1984)

The CMC algorithm operates in the [Lab color space](spaces-lab.md) and has two modes: `Acceptability` and `Imperceptibility`.

```csharp
var differenceThreshold = CMCColorDifferenceThreshold.Acceptability; // or "Imperceptibility"
var differenceCalculator = new CMCColorDifference(differenceThreshold);
var labColor1 = new LabColor(55, 80, 50);
var labColor2 = new LabColor(18, 36, -60);
double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 69.7388
```

### Related links

- http://www.brucelindbloom.com/index.html?Eqn_DeltaE_CMC.html
- https://en.wikipedia.org/wiki/Color_difference#CMC_l:c_(1984)


## CIE Delta-E 1994

The CIE94 algorithm operates in the [Lab color space](spaces-lab.md) and has two modes: `GraphicArts` and `Textiles`.

```csharp
var application = CIE94ColorDifferenceApplication.GraphicArts; // or "Textiles"
var differenceCalculator = new CIE94ColorDifference(application);
var labColor1 = new LabColor(55, 80, 50);
var labColor2 = new LabColor(18, 36, -60);
double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 60.7882
```

### Related links

- http://www.brucelindbloom.com/Eqn_DeltaE_CIE94.html
- https://en.wikipedia.org/wiki/Color_difference#CIE94


## CIE Delta-E 2000

The CIEDE2000 algorithm operates in the [Lab color space](spaces-lab.md).

```csharp
var differenceCalculator = new CIEDE2000ColorDifference();
var labColor1 = new LabColor(55, 80, 50);
var labColor2 = new LabColor(18, 36, -60);
double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 52.2320
```

### Related links

- http://www.brucelindbloom.com/Eqn_DeltaE_CIE2000.html
- https://en.wikipedia.org/wiki/Color_difference#CIEDE2000


## J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> Delta-E<sub>z</sub>

The ΔE<sub>z</sub> algorithm operates on the [J<sub>z</sub>C<sub>z</sub>h<sub>z</sub> color space](spaces-jzazbz.md), a [cylindrical variant](topic-cylindrical-spaces.md) of [J<sub>z</sub>a<sub>z</sub>b<sub>z</sub>](spaces-jzazbz.md).

```csharp
var differenceCalculator = new JzCzhzDEzColorDifference();
var color1 = new JzCzhzColor(0.3, 0.4, 165);
var color2 = new JzCzhzColor(0.8, 0.6, 25);
double difference = differenceCalculator.ComputeDifference(in color1, in color2); // 1.0666
```

### Related links

- https://observablehq.com/@jrus/jzazbz


## Euclidean distance

The Euclidean distance is a generic algorithm that can operate in any color space. 

```csharp
// example for euclidean distance in the XYZ color space
var differenceCalculator = new EuclideanDistanceColorDifference<XYZColor>();
var color1 = new XYZColor(0.5, 0.5, 0.5);
var color2 = new XYZColor(0.2, 0.4, 0.6);
double difference = differenceCalculator.ComputeDifference(in color1, in color2); // 0.3317
```

### Related links

- https://en.wikipedia.org/wiki/Euclidean_distance
