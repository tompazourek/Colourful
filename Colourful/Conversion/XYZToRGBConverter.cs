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
    /// Converts from <see cref="XYZColor"/> to <see cref="RGBColor"/>.
    /// </summary>
    /// <remarks>
    /// The target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/> when not set.
    /// If the source XYZ color reference white doesn't match the target RGB working space reference white, it is adjusted using <see cref="RGBAndXYZConverterBase.ChromaticAdaptation"/>.
    /// </remarks>
    public class XYZToRGBConverter : RGBAndXYZConverterBase, IColorConverter<XYZColor, RGBColor>
    {
        public XYZToRGBConverter()
        {
        }

        public XYZToRGBConverter(IRGBWorkingSpace rgbWorkingSpace)
        {
            RGBWorkingSpace = rgbWorkingSpace;
        }

        public XYZToRGBConverter(IChromaticAdaptation chromaticAdaptation) : base(chromaticAdaptation)
        {
        }

        public XYZToRGBConverter(IRGBWorkingSpace rgbWorkingSpace, IChromaticAdaptation chromaticAdaptation)
            : base(chromaticAdaptation)
        {
            RGBWorkingSpace = rgbWorkingSpace;
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace RGBWorkingSpace { get; private set; }

        public RGBColor Convert(XYZColor input)
        {
            RGBColor result = Convert(input, RGBWorkingSpace ?? RGBColor.DefaultWorkingSpace);
            return result;
        }

        private RGBColor Convert(XYZColor input, IRGBWorkingSpace workingSpace)
        {
            Vector<double> inputVector;

            if (input.ReferenceWhite == workingSpace.ReferenceWhite)
                inputVector = input.Vector;
            else
                inputVector = ChromaticAdaptation.TransformNonCropped(input, workingSpace.ReferenceWhite).Vector;

            Vector<double> uncompandedVector = GetXYZToRGBMatrix(workingSpace) * inputVector;
            RGBColor result = CompandVector(uncompandedVector, workingSpace);
            return result;
        }

        /// <summary>
        /// Applying the working space companding function (<see cref="IRGBWorkingSpace.Companding"/>) to uncompanded vector.
        /// </summary>
        private static RGBColor CompandVector(Vector<double> uncompandedVector, IRGBWorkingSpace workingSpace)
        {
            ICompanding companding = workingSpace.Companding;
            DenseVector compandedVector = DenseVector.OfEnumerable(uncompandedVector.Select(companding.Companding));
            double R, G, B;
            compandedVector.AssignVariables(out R, out G, out B);

            R = R.CropRange(0, 1);
            G = G.CropRange(0, 1);
            B = B.CropRange(0, 1);

            var result = new RGBColor(R, G, B, workingSpace);
            return result;
        }
    }
}