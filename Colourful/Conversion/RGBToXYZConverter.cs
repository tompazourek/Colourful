using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.RGBWorkingSpaces;

namespace Colourful.Conversion
{
    public class RGBToXYZConverter : IColorConverter<RGBColor, XYZColor>
    {
        /// <summary>
        /// Converts RGB to XYZ, target reference white is taken from RGB working space
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public XYZColor Convert(RGBColor input)
        {
            IRGBWorkingSpace workingSpace = input.WorkingSpace;

            var rgb = input.GetUncompandedVector();
            var matrix = workingSpace.GetRGBToXYZMatrix();

            var xyz = matrix*rgb;

            double x, y, z;
            xyz.AssignVariables(out x, out y, out z);

            var referenceWhite = workingSpace.ReferenceWhite;

            return new XYZColor(x, y, z, referenceWhite);
        }

        /// <summary>
        /// Converts RGB to XYZ, output color is adjusted to the given reference white (Bradford adaptation)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="referenceWhite"></param>
        /// <returns></returns>
        public XYZColor Convert(RGBColor input, XYZColorBase referenceWhite)
        {
            var converted = Convert(input);

            if (converted.ReferenceWhite == referenceWhite)
                return converted;

            var output = new BradfordChromaticAdaptation().Transform(converted, referenceWhite);
            return output;
        }
    }
}
