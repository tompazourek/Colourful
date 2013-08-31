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
    /// Chromatic adaptation.
    /// A linear transformation of a source color (XS, YS, ZS) into a destination color (XD, YD, ZD) by a linear transformation [M]
    /// which is dependent on the source reference white (XWS, YWS, ZWS) and the destination reference white (XWD, YWD, ZWD).
    /// </summary>
    public abstract class ChromaticAdaptationBase : IChromaticAdaptation
    {
        /// <summary>
        /// Definition of the cone response domain.
        /// </summary>
        public abstract Matrix<double> MA { get; }

        public XYZColor Transform(XYZColor source, XYZColorBase destinationReferenceWhite)
        {
            double XD, YD, ZD;
            TransformCore(source, destinationReferenceWhite, out XD, out YD, out ZD);

            XD = XD.CropRange(0, destinationReferenceWhite.X);
            YD = YD.CropRange(0, destinationReferenceWhite.Y);
            ZD = ZD.CropRange(0, destinationReferenceWhite.Z);

            return new XYZColor(XD, YD, ZD, destinationReferenceWhite);
        }

        private void TransformCore(XYZColor source, XYZColorBase destinationReferenceWhite, out double XD, out double YD, out double ZD)
        {
            // transformation described here: http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
            double rhoS, gammaS, betaS, rhoD, gammaD, betaD;
            (MA * source.ReferenceWhite.Vector).AssignVariables(out rhoS, out gammaS, out betaS);
            (MA * destinationReferenceWhite.Vector).AssignVariables(out rhoD, out gammaD, out betaD);

            DiagonalMatrix diagonalMatrix = DiagonalMatrix.OfDiagonal(3, 3, new[] { rhoD / rhoS, gammaD / gammaS, betaD / betaS });
            Matrix<double> M = MA.Inverse() * diagonalMatrix * MA;

            (M * source.Vector).AssignVariables(out XD, out YD, out ZD);
        }

        public XYZColorBase TransformNonCropped(XYZColor source, XYZColorBase destinationReferenceWhite)
        {
            double XD, YD, ZD;
            TransformCore(source, destinationReferenceWhite, out XD, out YD, out ZD);

            return new XYZColorBase(XD, YD, ZD);
        }
    }
}