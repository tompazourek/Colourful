using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using Colourful.Implementation.RGB;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from <see cref="RGBColor"/> to <see cref="XYZColor"/>.
    /// </summary>
    public class RGBToXYZConverter : RGBAndXYZConverterBase, IColorConverter<RGBColor, XYZColor>
    {
        private readonly Matrix<double> _conversionMatrix;

        /// <param name="sourceRGBWorkingSpace">Source RGB working space</param>
        public RGBToXYZConverter(IRGBWorkingSpace sourceRGBWorkingSpace)
        {
            SourceRGBWorkingSpace = sourceRGBWorkingSpace;
            _conversionMatrix = GetRGBToXYZMatrix(SourceRGBWorkingSpace);
        }

        /// <summary>
        /// Source RGB working space
        /// </summary>
        public IRGBWorkingSpace SourceRGBWorkingSpace { get; private set; }

        public XYZColor Convert(RGBColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            if (!RGBWorkingSpaceEqualityComparer.Default.Equals(input.WorkingSpace, SourceRGBWorkingSpace))
                throw new InvalidOperationException("Working space of input RGB color must be equal to converter source RGB working space.");

            Vector<double> rgb = UncompandVector(input);
            Vector<double> xyz = _conversionMatrix * rgb;

            double x, y, z;
            xyz.AssignVariables(out x, out y, out z);

            var converted = new XYZColor(x, y, z);
            return converted;
        }

        /// <summary>
        /// Applying the working space inverse companding function (<see cref="IRGBWorkingSpace.Companding"/>) to RGB vector.
        /// </summary>
        private static Vector<double> UncompandVector(RGBColor rgbColor)
        {
            ICompanding inverseCompanding = rgbColor.WorkingSpace.Companding;
            Vector<double> compandedVector = rgbColor.Vector;
            DenseVector uncompandedVector = DenseVector.OfEnumerable(compandedVector.Select(inverseCompanding.InverseCompanding));
            return uncompandedVector;
        }
    }
}