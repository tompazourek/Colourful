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
    }
}