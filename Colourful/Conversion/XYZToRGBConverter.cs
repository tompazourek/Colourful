using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.ChromaticAdaptation;
using Colourful.Colors;
using Colourful.Implementation.RGB;
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
        private readonly Matrix<double> _conversionMatrix;

        public XYZToRGBConverter() : this((IRGBWorkingSpace)null)
        {
        }

        /// <param name="chromaticAdaptation">When not set, <see cref="RGBAndXYZConverterBase.DefaultChromaticAdaptation"/></param>
        public XYZToRGBConverter(IChromaticAdaptation chromaticAdaptation) : this(null, chromaticAdaptation)
        {
        }

        /// <param name="targetRGBWorkingSpace">When not set, <see cref="RGBColor.DefaultWorkingSpace"/></param>
        public XYZToRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace) : this(targetRGBWorkingSpace, null)
        {
        }

        /// <param name="targetRGBWorkingSpace">When not set, <see cref="RGBColor.DefaultWorkingSpace"/></param>
        /// <param name="chromaticAdaptation">When not set, <see cref="RGBAndXYZConverterBase.DefaultChromaticAdaptation"/></param>
        public XYZToRGBConverter(IRGBWorkingSpace targetRGBWorkingSpace, IChromaticAdaptation chromaticAdaptation)
        {
            TargetRGBWorkingSpace = targetRGBWorkingSpace ?? RGBColor.DefaultWorkingSpace;
            ChromaticAdaptation = chromaticAdaptation ?? DefaultChromaticAdaptation;

            _conversionMatrix = GetXYZToRGBMatrix(TargetRGBWorkingSpace);
        }

        /// <summary>
        /// Target RGB working space. When not set, target RGB working space is <see cref="RGBColor.DefaultWorkingSpace"/>.
        /// </summary>
        public IRGBWorkingSpace TargetRGBWorkingSpace { get; private set; }

        public RGBColor Convert(XYZColor input)
        {
            if (input == null) throw new ArgumentNullException("input");

            Vector<double> inputVector;

            if (input.ReferenceWhite == TargetRGBWorkingSpace.ReferenceWhite)
                inputVector = input.Vector;
            else
                inputVector = ChromaticAdaptation.TransformNonCropped(input, TargetRGBWorkingSpace.ReferenceWhite).Vector;

            Vector<double> uncompandedVector = _conversionMatrix * inputVector;
            RGBColor result1 = CompandVector(uncompandedVector, TargetRGBWorkingSpace);
            RGBColor result = result1;
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