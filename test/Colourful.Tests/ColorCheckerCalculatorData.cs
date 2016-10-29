﻿#region License

// Copyright (C) Tomáš Pažourek, 2014
// All rights reserved.
//
// Distributed under MIT license as a part of project Colourful.
// https://github.com/tompazourek/Colourful

#endregion License

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
        public static IEnumerable<Row> GetData()
        {
            return new List<Row>
            {
                new Row("Dark Skin", 0.111082, 0.097287, 0.067379, 0.402837, 0.352813, 37.351132, 11.955004, 15.019788, 19.196775, 51.481955, 24.172043, 16.068394, 29.025522, 33.614021, 0.450152, 0.311969, 0.252390), 
                new Row("Light Skin", 0.385061, 0.353261, 0.281610, 0.377536, 0.346358, 66.001794, 12.668171, 17.407313, 21.528983, 53.954747, 30.050236, 22.384803, 37.471271, 36.682894, 0.763580, 0.591622, 0.510392), 
                new Row("Blue Sky", 0.184244, 0.188767, 0.373784, 0.246713, 0.252770, 50.542673, -0.459301, -21.517668, 21.522569, 268.777190, -14.950781, -33.005720, 36.234009, 245.630641, 0.367373, 0.481970, 0.612783), 
                new Row("Foilage", 0.106306, 0.131860, 0.075357, 0.339069, 0.420575, 43.042144, -16.092885, 21.906721, 27.182446, 126.301357, -9.419558, 29.541204, 31.006626, 107.685490, 0.345053, 0.425476, 0.254160), 
                new Row("Blue Flower", 0.260129, 0.235993, 0.486923, 0.264616, 0.240063, 55.684348, 12.269654, -25.207076, 28.034641, 295.954704, -2.247710, -41.373922, 41.434932, 266.890364, 0.509593, 0.505533, 0.692617), 
                new Row("Bluish Green", 0.319735, 0.422109, 0.485475, 0.260515, 0.343928, 71.016112, -30.945216, 1.374675, 30.975735, 177.456430, -39.836824, 7.086017, 40.462132, 169.913943, 0.390923, 0.745839, 0.669201), 
                new Row("Orange", 0.366308, 0.291261, 0.058755, 0.511372, 0.406605, 60.892733, 28.647374, 59.043276, 65.626066, 64.117671, 77.130691, 57.655799, 96.298155, 36.778404, 0.850470, 0.477372, 0.146072), 
                new Row("Purplish Blue", 0.139714, 0.115995, 0.406848, 0.210870, 0.175072, 40.572374, 17.288414, -42.612239, 45.985782, 292.083121, -10.879594, -65.478538, 66.376234, 260.566198, 0.282714, 0.356371, 0.648257), 
                new Row("Moderate Red", 0.283763, 0.188953, 0.148058, 0.457111, 0.304383, 50.564497, 43.786327, 14.705888, 46.189886, 18.564920, 77.396684, 10.850709, 78.153596, 7.980627, 0.761862, 0.329372, 0.382479), 
                new Row("Purple", 0.089934, 0.065176, 0.161692, 0.283881, 0.205732, 30.682589, 24.254374, -22.554751, 33.120861, 317.079476, 12.284838, -33.141266, 35.344883, 290.338787, 0.357362, 0.233317, 0.419629), 
                new Row("Yellow Green", 0.339048, 0.433949, 0.116832, 0.381026, 0.487677, 71.822198, -27.625924, 58.953619, 65.105459, 115.107994, -11.671420, 76.230520, 77.118832, 98.704782, 0.628242, 0.735771, 0.236473), 
                new Row("Orange Yellow", 0.472852, 0.437261, 0.087677, 0.473900, 0.438230, 72.045061, 12.562705, 67.775975, 68.930431, 79.499044, 54.687072, 73.605946, 91.697934, 53.388727, 0.902928, 0.640479, 0.166143), 
                new Row("Blue", 0.090454, 0.061383, 0.335965, 0.185432, 0.125836, 29.758774, 28.670256, -52.592666, 59.899683, 298.596476, -8.392862, -72.448910, 72.933427, 263.392011, 0.182221, 0.233593, 0.599351), 
                new Row("Green", 0.148031, 0.231601, 0.100475, 0.308329, 0.482394, 55.236883, -40.838036, 34.891980, 53.714015, 139.489454, -35.884928, 50.540334, 61.984300, 125.375725, 0.278578, 0.586481, 0.270667), 
                new Row("Red", 0.203465, 0.116458, 0.053125, 0.545412, 0.312179, 40.647475, 51.823192, 26.565420, 58.235425, 27.140423, 97.692479, 18.982960, 99.519713, 10.996307, 0.694617, 0.173810, 0.218710), 
                new Row("Yellow", 0.573688, 0.598675, 0.101608, 0.450315, 0.469928, 81.766136, -3.244259, 80.302967, 80.368475, 92.313505, 33.880341, 91.042224, 97.141979, 69.587815, 0.934351, 0.785446, 0.105858), 
                new Row("Magenta", 0.299155, 0.189630, 0.329030, 0.365798, 0.231874, 50.643924, 49.318463, -15.672781, 51.748883, 342.370405, 58.461543, -31.414798, 66.367473, 331.748268, 0.732377, 0.322881, 0.579329), 
                new Row("Cyan", 0.148614, 0.191322, 0.420954, 0.195316, 0.251445, 50.841498, -21.543727, -26.508235, 34.158728, 230.898610, -41.006648, -38.793123, 56.448664, 223.411109, -0.191866, 0.528371, 0.649604), 
                new Row("White", 0.864034, 0.885010, 1.005567, 0.313668, 0.321283, 95.371488, -0.725688, 2.528807, 2.630872, 106.011819, 0.697850, 4.207561, 4.265039, 80.582862, 0.951248, 0.948524, 0.927750), 
                new Row("Neutral 8", 0.573045, 0.584464, 0.688433, 0.310435, 0.316621, 80.986351, -0.039431, 0.209266, 0.212949, 100.670780, 0.087275, 0.337611, 0.348709, 75.505903, 0.788934, 0.788520, 0.786161), 
                new Row("Neutral 6.5", 0.349243, 0.356443, 0.421254, 0.309904, 0.316293, 66.247244, -0.113354, 0.019735, 0.115059, 170.123593, -0.144472, 0.049219, 0.152626, 161.187093, 0.630668, 0.631674, 0.630735), 
                new Row("Neutral 5", 0.190881, 0.194819, 0.226458, 0.311816, 0.318250, 51.246289, -0.094104, 0.654768, 0.661496, 98.178624, 0.286657, 0.957532, 0.999520, 73.333836, 0.480246, 0.478619, 0.474046), 
                new Row("Neutral 3.5", 0.085078, 0.086898, 0.103279, 0.309088, 0.315699, 35.380005, -0.126605, -0.154372, 0.199648, 230.643912, -0.238105, -0.183234, 0.300448, 217.580198, 0.324919, 0.326521, 0.326944), 
                new Row("Black", 0.030820, 0.031217, 0.037260, 0.310383, 0.314377, 20.524662, 0.349927, -0.199621, 0.402861, 330.296782, 0.246834, -0.253838, 0.354063, 314.198587, 0.195356, 0.193241, 0.194901), 
            };
        }

        public static XYZColor GetXYZColor(this Row row)
        {
            return new XYZColor(row.X, row.Y, row.Z);
        }

        public static RGBColor GetRGBColor(this Row row)
        {
            return new RGBColor(row.R.CropRange(0, 1), row.G.CropRange(0, 1), row.B.CropRange(0, 1), RGBWorkingSpaces.sRGB);
        }

        public static LabColor GetLabColor(this Row row)
        {
            return new LabColor(row.L, row.a, row.b, Illuminants.C);
        }

        public static LabColor GetLuvColor(this Row row)
        {
            return new LabColor(row.L, row.u, row.v, Illuminants.C);
        }

        public static xyYColor GetxyYColor(this Row row)
        {
            return new xyYColor(row.x_chromaticity, row.y_chromaticity, row.Y);
        }

        public static LChabColor GetLChabColor(this Row row)
        {
            return new LChabColor(row.L, row.C_ab, row.H_ab, Illuminants.C);
        }

        public static LChuvColor GetLChuvColor(this Row row)
        {
            return new LChuvColor(row.L, row.C_uv, row.H_uv, Illuminants.C);
        }

        public static double CropRange(this double value, double min, double max)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        public class Row
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public double x_chromaticity { get; set; }
            public double y_chromaticity { get; set; }
            public double L { get; set; }
            public double a { get; set; }
            public double b { get; set; }
            public double C_ab { get; set; }
            public double H_ab { get; set; }
            public double u { get; set; }
            public double v { get; set; }
            public double C_uv { get; set; }
            public double H_uv { get; set; }
            public double R { get; set; }
            public double G { get; set; }
            public double B { get; set; }

            public Row(string name, double X, double Y, double Z, double x_chromaticity, double y_chromaticity, double L, double a, double b, double C_ab, double H_ab, double u, double v, double C_uv, double H_uv, double R, double G, double B)
            {
                this.Name = name;
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
        }
    }
}