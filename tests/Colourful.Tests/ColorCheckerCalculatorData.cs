using System.Collections.Generic;

namespace Colourful.Tests
{
    /// <summary>
    /// Test data gathered from: http://www.brucelindbloom.com/index.html?ColorCheckerCalculator.html
    /// </summary>
    /// <remarks>
    /// Ref. Illuminant: C, RGB Model: sRGB, Std. Observer: 2 deg.
    /// </remarks>
    public static class ColorCheckerCalculatorData
    {
        public static IEnumerable<Row> GetData() => new List<Row>
        {
            new Row("Dark Skin", X: 0.111082, Y: 0.097287, Z: 0.067379, x_chromaticity: 0.402837, y_chromaticity: 0.352813, L: 37.351132, a: 11.955004, b: 15.019788, C_ab: 19.196775, H_ab: 51.481955, u: 24.172043, v: 16.068394, C_uv: 29.025522, H_uv: 33.614021, R: 0.450152, G: 0.311969, B: 0.252390),
            new Row("Light Skin", X: 0.385061, Y: 0.353261, Z: 0.281610, x_chromaticity: 0.377536, y_chromaticity: 0.346358, L: 66.001794, a: 12.668171, b: 17.407313, C_ab: 21.528983, H_ab: 53.954747, u: 30.050236, v: 22.384803, C_uv: 37.471271, H_uv: 36.682894, R: 0.763580, G: 0.591622, B: 0.510392),
            new Row("Blue Sky", X: 0.184244, Y: 0.188767, Z: 0.373784, x_chromaticity: 0.246713, y_chromaticity: 0.252770, L: 50.542673, a: -0.459301, b: -21.517668, C_ab: 21.522569, H_ab: 268.777190, u: -14.950781, v: -33.005720, C_uv: 36.234009, H_uv: 245.630641, R: 0.367373, G: 0.481970, B: 0.612783),
            new Row("Foilage", X: 0.106306, Y: 0.131860, Z: 0.075357, x_chromaticity: 0.339069, y_chromaticity: 0.420575, L: 43.042144, a: -16.092885, b: 21.906721, C_ab: 27.182446, H_ab: 126.301357, u: -9.419558, v: 29.541204, C_uv: 31.006626, H_uv: 107.685490, R: 0.345053, G: 0.425476, B: 0.254160),
            new Row("Blue Flower", X: 0.260129, Y: 0.235993, Z: 0.486923, x_chromaticity: 0.264616, y_chromaticity: 0.240063, L: 55.684348, a: 12.269654, b: -25.207076, C_ab: 28.034641, H_ab: 295.954704, u: -2.247710, v: -41.373922, C_uv: 41.434932, H_uv: 266.890364, R: 0.509593, G: 0.505533, B: 0.692617),
            new Row("Bluish Green", X: 0.319735, Y: 0.422109, Z: 0.485475, x_chromaticity: 0.260515, y_chromaticity: 0.343928, L: 71.016112, a: -30.945216, b: 1.374675, C_ab: 30.975735, H_ab: 177.456430, u: -39.836824, v: 7.086017, C_uv: 40.462132, H_uv: 169.913943, R: 0.390923, G: 0.745839, B: 0.669201),
            new Row("Orange", X: 0.366308, Y: 0.291261, Z: 0.058755, x_chromaticity: 0.511372, y_chromaticity: 0.406605, L: 60.892733, a: 28.647374, b: 59.043276, C_ab: 65.626066, H_ab: 64.117671, u: 77.130691, v: 57.655799, C_uv: 96.298155, H_uv: 36.778404, R: 0.850470, G: 0.477372, B: 0.146072),
            new Row("Purplish Blue", X: 0.139714, Y: 0.115995, Z: 0.406848, x_chromaticity: 0.210870, y_chromaticity: 0.175072, L: 40.572374, a: 17.288414, b: -42.612239, C_ab: 45.985782, H_ab: 292.083121, u: -10.879594, v: -65.478538, C_uv: 66.376234, H_uv: 260.566198, R: 0.282714, G: 0.356371, B: 0.648257),
            new Row("Moderate Red", X: 0.283763, Y: 0.188953, Z: 0.148058, x_chromaticity: 0.457111, y_chromaticity: 0.304383, L: 50.564497, a: 43.786327, b: 14.705888, C_ab: 46.189886, H_ab: 18.564920, u: 77.396684, v: 10.850709, C_uv: 78.153596, H_uv: 7.980627, R: 0.761862, G: 0.329372, B: 0.382479),
            new Row("Purple", X: 0.089934, Y: 0.065176, Z: 0.161692, x_chromaticity: 0.283881, y_chromaticity: 0.205732, L: 30.682589, a: 24.254374, b: -22.554751, C_ab: 33.120861, H_ab: 317.079476, u: 12.284838, v: -33.141266, C_uv: 35.344883, H_uv: 290.338787, R: 0.357362, G: 0.233317, B: 0.419629),
            new Row("Yellow Green", X: 0.339048, Y: 0.433949, Z: 0.116832, x_chromaticity: 0.381026, y_chromaticity: 0.487677, L: 71.822198, a: -27.625924, b: 58.953619, C_ab: 65.105459, H_ab: 115.107994, u: -11.671420, v: 76.230520, C_uv: 77.118832, H_uv: 98.704782, R: 0.628242, G: 0.735771, B: 0.236473),
            new Row("Orange Yellow", X: 0.472852, Y: 0.437261, Z: 0.087677, x_chromaticity: 0.473900, y_chromaticity: 0.438230, L: 72.045061, a: 12.562705, b: 67.775975, C_ab: 68.930431, H_ab: 79.499044, u: 54.687072, v: 73.605946, C_uv: 91.697934, H_uv: 53.388727, R: 0.902928, G: 0.640479, B: 0.166143),
            new Row("Blue", X: 0.090454, Y: 0.061383, Z: 0.335965, x_chromaticity: 0.185432, y_chromaticity: 0.125836, L: 29.758774, a: 28.670256, b: -52.592666, C_ab: 59.899683, H_ab: 298.596476, u: -8.392862, v: -72.448910, C_uv: 72.933427, H_uv: 263.392011, R: 0.182221, G: 0.233593, B: 0.599351),
            new Row("Green", X: 0.148031, Y: 0.231601, Z: 0.100475, x_chromaticity: 0.308329, y_chromaticity: 0.482394, L: 55.236883, a: -40.838036, b: 34.891980, C_ab: 53.714015, H_ab: 139.489454, u: -35.884928, v: 50.540334, C_uv: 61.984300, H_uv: 125.375725, R: 0.278578, G: 0.586481, B: 0.270667),
            new Row("Red", X: 0.203465, Y: 0.116458, Z: 0.053125, x_chromaticity: 0.545412, y_chromaticity: 0.312179, L: 40.647475, a: 51.823192, b: 26.565420, C_ab: 58.235425, H_ab: 27.140423, u: 97.692479, v: 18.982960, C_uv: 99.519713, H_uv: 10.996307, R: 0.694617, G: 0.173810, B: 0.218710),
            new Row("Yellow", X: 0.573688, Y: 0.598675, Z: 0.101608, x_chromaticity: 0.450315, y_chromaticity: 0.469928, L: 81.766136, a: -3.244259, b: 80.302967, C_ab: 80.368475, H_ab: 92.313505, u: 33.880341, v: 91.042224, C_uv: 97.141979, H_uv: 69.587815, R: 0.934351, G: 0.785446, B: 0.105858),
            new Row("Magenta", X: 0.299155, Y: 0.189630, Z: 0.329030, x_chromaticity: 0.365798, y_chromaticity: 0.231874, L: 50.643924, a: 49.318463, b: -15.672781, C_ab: 51.748883, H_ab: 342.370405, u: 58.461543, v: -31.414798, C_uv: 66.367473, H_uv: 331.748268, R: 0.732377, G: 0.322881, B: 0.579329),
            new Row("Cyan", X: 0.148614, Y: 0.191322, Z: 0.420954, x_chromaticity: 0.195316, y_chromaticity: 0.251445, L: 50.841498, a: -21.543727, b: -26.508235, C_ab: 34.158728, H_ab: 230.898610, u: -41.006648, v: -38.793123, C_uv: 56.448664, H_uv: 223.411109, R: -0.191866, G: 0.528371, B: 0.649604),
            new Row("White", X: 0.864034, Y: 0.885010, Z: 1.005567, x_chromaticity: 0.313668, y_chromaticity: 0.321283, L: 95.371488, a: -0.725688, b: 2.528807, C_ab: 2.630872, H_ab: 106.011819, u: 0.697850, v: 4.207561, C_uv: 4.265039, H_uv: 80.582862, R: 0.951248, G: 0.948524, B: 0.927750),
            new Row("Neutral 8", X: 0.573045, Y: 0.584464, Z: 0.688433, x_chromaticity: 0.310435, y_chromaticity: 0.316621, L: 80.986351, a: -0.039431, b: 0.209266, C_ab: 0.212949, H_ab: 100.670780, u: 0.087275, v: 0.337611, C_uv: 0.348709, H_uv: 75.505903, R: 0.788934, G: 0.788520, B: 0.786161),
            new Row("Neutral 6.5", X: 0.349243, Y: 0.356443, Z: 0.421254, x_chromaticity: 0.309904, y_chromaticity: 0.316293, L: 66.247244, a: -0.113354, b: 0.019735, C_ab: 0.115059, H_ab: 170.123593, u: -0.144472, v: 0.049219, C_uv: 0.152626, H_uv: 161.187093, R: 0.630668, G: 0.631674, B: 0.630735),
            new Row("Neutral 5", X: 0.190881, Y: 0.194819, Z: 0.226458, x_chromaticity: 0.311816, y_chromaticity: 0.318250, L: 51.246289, a: -0.094104, b: 0.654768, C_ab: 0.661496, H_ab: 98.178624, u: 0.286657, v: 0.957532, C_uv: 0.999520, H_uv: 73.333836, R: 0.480246, G: 0.478619, B: 0.474046),
            new Row("Neutral 3.5", X: 0.085078, Y: 0.086898, Z: 0.103279, x_chromaticity: 0.309088, y_chromaticity: 0.315699, L: 35.380005, a: -0.126605, b: -0.154372, C_ab: 0.199648, H_ab: 230.643912, u: -0.238105, v: -0.183234, C_uv: 0.300448, H_uv: 217.580198, R: 0.324919, G: 0.326521, B: 0.326944),
            new Row("Black", X: 0.030820, Y: 0.031217, Z: 0.037260, x_chromaticity: 0.310383, y_chromaticity: 0.314377, L: 20.524662, a: 0.349927, b: -0.199621, C_ab: 0.402861, H_ab: 330.296782, u: 0.246834, v: -0.253838, C_uv: 0.354063, H_uv: 314.198587, R: 0.195356, G: 0.193241, B: 0.194901),
        };

        public static XYZColor GetXYZColor(this Row row) => new XYZColor(row.X, row.Y, row.Z);

        public static RGBColor GetRGBColor(this Row row) => new RGBColor(row.R, row.G, row.B);

        public static LabColor GetLabColor(this Row row) => new LabColor(row.L, row.a, row.b);

        public static LabColor GetLuvColor(this Row row) => new LabColor(row.L, row.u, row.v);

        public static xyYColor GetxyYColor(this Row row) => new xyYColor(row.x_chromaticity, row.y_chromaticity, row.Y);

        public static LChabColor GetLChabColor(this Row row) => new LChabColor(row.L, row.C_ab, row.H_ab);

        public static LChuvColor GetLChuvColor(this Row row) => new LChuvColor(row.L, row.C_uv, row.H_uv);

        public class Row
        {
            public Row(in string name, in double X, in double Y, in double Z, in double x_chromaticity, in double y_chromaticity, in double L, in double a, in double b, in double C_ab, in double H_ab, in double u, in double v, in double C_uv, in double H_uv, in double R, in double G, in double B)
            {
                Name = name;
                this.X = X;
                this.Y = Y;
                this.Z = Z;
                this.x_chromaticity = x_chromaticity;
                this.y_chromaticity = y_chromaticity;
                this.L = L;
                this.a = a;
                this.b = b;
                this.C_ab = C_ab;
                this.H_ab = H_ab;
                this.u = u;
                this.v = v;
                this.C_uv = C_uv;
                this.H_uv = H_uv;
                this.R = R;
                this.G = G;
                this.B = B;
            }

            public string Name { get; }
            public double X { get; }
            public double Y { get; }
            public double Z { get; }
            public double x_chromaticity { get; }
            public double y_chromaticity { get; }
            public double L { get; }
            public double a { get; }
            public double b { get; }
            public double C_ab { get; }
            public double H_ab { get; }
            public double u { get; }
            public double v { get; }
            public double C_uv { get; }
            public double H_uv { get; }
            public double R { get; }
            public double G { get; }
            public double B { get; }
        }
    }
}