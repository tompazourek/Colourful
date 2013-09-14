using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;
using Colourful.Colors;
using Colourful.RGBWorkingSpaces;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts from <see cref="RGBColor"/> to <see cref="XYZColor"/>.
    /// </summary>
    /// <remarks>
    /// Target reference white is <see cref="XYZColor.ReferenceWhite"/>, when not set, reference white is taken from RGB working space.
    /// Conversion to target reference white is done via <see cref="RGBAndXYZConverterBase.ChromaticAdaptation"/>, when not set, <see cref="RGBAndXYZConverterBase.DefaultChromaticAdaptation"/> is used.
    /// </remarks>
    public class RGBToXYZConverter : RGBAndXYZConverterBase, IColorConverter<RGBColor, XYZColor>
    {
        public RGBToXYZConverter()
        {
        }

        public RGBToXYZConverter(XYZColorBase referenceWhite)
        {
            ReferenceWhite = referenceWhite;
        }

        public RGBToXYZConverter(IChromaticAdaptation chromaticAdaptation) : base(chromaticAdaptation)
        {
        }

        public RGBToXYZConverter(XYZColorBase referenceWhite, IChromaticAdaptation chromaticAdaptation)
            : base(chromaticAdaptation)
        {
            ReferenceWhite = referenceWhite;
        }

        /// <summary>
        /// Target reference white. When not set, reference white is taken from RGB working space.
        /// </summary>
        public XYZColorBase ReferenceWhite { get; private set; }

        public XYZColor Convert(RGBColor input)
        {
            IRGBWorkingSpace workingSpace = input.WorkingSpace;

            Vector<double> rgb = UncompandVector(input);
            Matrix<double> matrix = GetRGBToXYZMatrix(workingSpace);

            Vector<double> xyz = matrix * rgb;

            double x, y, z;
            xyz.AssignVariables(out x, out y, out z);

            var converted = new XYZColor(x, y, z, workingSpace.ReferenceWhite);
            if (ReferenceWhite == null || workingSpace.ReferenceWhite == ReferenceWhite)
                return converted;

            XYZColor output = ChromaticAdaptation.Transform(converted, ReferenceWhite);
            return output;
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