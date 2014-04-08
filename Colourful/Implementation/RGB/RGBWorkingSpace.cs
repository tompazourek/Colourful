using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.Implementation.RGB
{
    /// <summary>
    /// Trivial implementation of <see cref="IRGBWorkingSpace"/>
    /// </summary>
    public class RGBWorkingSpace : IRGBWorkingSpace
    {
        public RGBWorkingSpace(XYZColor referenceWhite, ICompanding companding, RGBPrimariesChromaticityCoordinates chromaticityCoordinates)
        {
            WhitePoint = referenceWhite;
            Companding = companding;
            ChromaticityCoordinates = chromaticityCoordinates;
        }

        public XYZColor WhitePoint { get; private set; }

        public RGBPrimariesChromaticityCoordinates ChromaticityCoordinates { get; private set; }

        public ICompanding Companding { get; private set; }
    }
}