using Colourful;
using Colourful.Implementation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorful.Net45
{
    public static class ColorExtensions
    {
        public static RGBColor AsRGBColor(Color color, IRGBWorkingSpace workingSpace)
        {
            return new RGBColor(((double)color.R) / 255, ((double)color.G) / 255, ((double)color.B) / 255, workingSpace);
        }

        public static RGBColor AsRGBColor(Color color)
        {
            return AsRGBColor(color, RGBColor.DefaultWorkingSpace);
        }

        public static Color ToColor(this RGBColor input)
        {
            if (input == null)
                return new Color();

            var r = (byte)Math.Round(input.R * 255).CropRange(0, 255);
            var g = (byte)Math.Round(input.G * 255).CropRange(0, 255);
            var b = (byte)Math.Round(input.B * 255).CropRange(0, 255);
            var output = Color.FromArgb(r, g, b);
            return output;
        }
    }
}
