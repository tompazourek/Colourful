using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    public interface IRGBWorkingSpace
    {
        XYZColorBase ReferenceWhite { get; }

        RGBSystemChromacityCoordinates ChromacityCoordinates { get; }

        /// <summary>
        /// The companded channel (R, G, B) is made linear with respect to the energy
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        double InverseCompanding(double channel);
    }
}