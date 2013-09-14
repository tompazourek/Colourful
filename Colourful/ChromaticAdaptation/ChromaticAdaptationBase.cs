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
        public abstract Matrix<double> MA { get; }

        /// <summary>
        /// Transforms XYZ color to destination reference white.
        /// </summary>
        public XYZColor Transform(XYZColor source, XYZColorBase destinationReferenceWhite)
        {
            if (destinationReferenceWhite == null)
                throw new ArgumentNullException("destinationReferenceWhite");

            double XD, YD, ZD;
            TransformCore(source, destinationReferenceWhite, out XD, out YD, out ZD);

            XD = XD.CropRange(0, destinationReferenceWhite.X);
            YD = YD.CropRange(0, destinationReferenceWhite.Y);
            ZD = ZD.CropRange(0, destinationReferenceWhite.Z);

            return new XYZColor(XD, YD, ZD, destinationReferenceWhite);
        }

        public XYZColorBase TransformNonCropped(XYZColor source, XYZColorBase destinationReferenceWhite)
        {
            double XD, YD, ZD;
            TransformCore(source, destinationReferenceWhite, out XD, out YD, out ZD);

            return new XYZColorBase(XD, YD, ZD);
        }

        private void TransformCore(XYZColor source, XYZColorBase destinationReferenceWhite, out double XD, out double YD, out double ZD)
        {
            double rhoS, gammaS, betaS, rhoD, gammaD, betaD;
            (MA * source.ReferenceWhite.Vector).AssignVariables(out rhoS, out gammaS, out betaS);
            (MA * destinationReferenceWhite.Vector).AssignVariables(out rhoD, out gammaD, out betaD);

            DiagonalMatrix diagonalMatrix = DiagonalMatrix.OfDiagonal(3, 3, new[] { rhoD / rhoS, gammaD / gammaS, betaD / betaS });
            Matrix<double> M = MA.Inverse() * diagonalMatrix * MA;

            (M * source.Vector).AssignVariables(out XD, out YD, out ZD);
        }
    }
}