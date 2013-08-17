using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.RGBWorkingSpaces;
using MathNet.Numerics.LinearAlgebra.Generic;

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

            Vector<double> rgb = input.GetUncompandedVector();
            Matrix<double> matrix = workingSpace.GetRGBToXYZMatrix();

            Vector<double> xyz = matrix * rgb;

            double x, y, z;
            xyz.AssignVariables(out x, out y, out z);

            XYZColorBase referenceWhite = workingSpace.ReferenceWhite;

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
            XYZColor converted = Convert(input);

            if (converted.ReferenceWhite == referenceWhite)
                return converted;

            XYZColor output = new BradfordChromaticAdaptation().Transform(converted, referenceWhite);
            return output;
        }
    }
}