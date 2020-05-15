using static System.Math;

namespace Colourful.Internals
{
    /// <inheritdoc />
    public class XYZToJzazbzConverter : IColorConverter<XYZColor, JzazbzColor>
    {
        /// <inheritdoc />
        public JzazbzColor Convert(in XYZColor sourceColor)
        {
            // conversion algorithm from: https://observablehq.com/@jrus/jzazbz

            var X = sourceColor.X * 10000d;
            var Y = sourceColor.Y * 10000d;
            var Z = sourceColor.Z * 10000d;

            var Lp = PerceptualQuantizer(0.674207838 * X + 0.382799340 * Y - 0.047570458 * Z);
            var Mp = PerceptualQuantizer(0.149284160 * X + 0.739628340 * Y + 0.083327300 * Z);
            var Sp = PerceptualQuantizer(0.070941080 * X + 0.174768000 * Y + 0.670970020 * Z);
            var Iz = 0.5 * (Lp + Mp);
            var az = 3.524000 * Lp - 4.066708 * Mp + 0.542708 * Sp;
            var bz = 0.199076 * Lp + 1.096799 * Mp - 1.295875 * Sp;
            var Jz = 0.44 * Iz / (1 - 0.56 * Iz) - 1.6295499532821566e-11;

            var targetColor = new JzazbzColor(in Jz, in az, in bz);
            return targetColor;
        }

        private static double PerceptualQuantizer(double x)
        {
            var xx = Pow(x * 1e-4, y: 0.1593017578125);
            var result = Pow((0.8359375 + 18.8515625 * xx) / (1 + 18.6875 * xx), y: 134.034375);
            return result;
        }
    }
}