using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.ChromaticAdaptation
{
    public interface IChromaticAdaptation
    {
        XYZColor Transform(XYZColor source, XYZColorBase destinationReferenceWhite);

        /// <remarks>Doesn't crop the resulting color space coordinates.</remarks>
        XYZColorBase TransformNonCropped(XYZColor source, XYZColorBase destinationReferenceWhite);
    }
}