using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace Colourful.Conversion
{
    public abstract class ChromaticAdaptationBase : IChromaticAdaptation
    {
        public abstract Matrix<double> MA { get; }

        public XYZColor Transform(XYZColor source, XYZColorBase destinationReferenceWhite)
        {
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            double rhoS, gammaS, betaS, rhoD, gammaD, betaD;
            (MA * source.ReferenceWhite.Vector).AssignVariables(out rhoS, out gammaS, out betaS);
            (MA * destinationReferenceWhite.Vector).AssignVariables(out rhoD, out gammaD, out betaD);

            DiagonalMatrix diagonalMatrix = DiagonalMatrix.OfDiagonal(3, 3, new[] { rhoD / rhoS, gammaD / gammaS, betaD / betaS });
            Matrix<double> M = MA.Inverse() * diagonalMatrix * MA;

            double XD, YD, ZD;
            (M * source.Vector).AssignVariables(out XD, out YD, out ZD);

            return new XYZColor(XD, YD, ZD, destinationReferenceWhite);
        }
    }
}