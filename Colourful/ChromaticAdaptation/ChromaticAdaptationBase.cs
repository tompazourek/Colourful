using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.ChromaticAdaptation
{
    /// <summary>
    /// Basic implementation of chromatic adaptation algorithm
    /// </summary>
    /// <remarks>
    /// Transformation described here:
    /// http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
    /// </remarks>
    public abstract class ChromaticAdaptationBase : IChromaticAdaptation
    {
        /// <summary>
        /// Definition of the cone response domain
        /// </summary>
        protected abstract Matrix<double> MA { get; }

        /// <summary>
        /// Transforms XYZ color to destination reference white.
        /// </summary>
        public XYZColor Transform(XYZColor sourceColor, XYZColor sourceWhitePoint, XYZColor targetWhitePoint)
        {
            if (sourceColor == null) throw new ArgumentNullException("sourceColor");
            if (sourceWhitePoint == null) throw new ArgumentNullException("sourceWhitePoint");
            if (targetWhitePoint == null) throw new ArgumentNullException("targetWhitePoint");

            if (sourceWhitePoint.Equals(targetWhitePoint))
                return sourceColor;

            double XD, YD, ZD;
            TransformCore(sourceColor, sourceWhitePoint, targetWhitePoint, out XD, out YD, out ZD);

            return new XYZColor(XD, YD, ZD);
        }

        private void TransformCore(XYZColor sourceColor, XYZColor sourceWhitePoint, XYZColor targetWhitePoint, out double XD, out double YD, out double ZD)
        {
            double rhoS, gammaS, betaS, rhoD, gammaD, betaD;
            (MA * sourceWhitePoint.Vector).AssignVariables(out rhoS, out gammaS, out betaS);
            (MA * targetWhitePoint.Vector).AssignVariables(out rhoD, out gammaD, out betaD);

            DiagonalMatrix diagonalMatrix = DiagonalMatrix.OfDiagonal(3, 3, new[] { rhoD / rhoS, gammaD / gammaS, betaD / betaS });
            Matrix<double> M = MA.Inverse() * diagonalMatrix * MA;

            (M * sourceColor.Vector).AssignVariables(out XD, out YD, out ZD);
        }
    }
}