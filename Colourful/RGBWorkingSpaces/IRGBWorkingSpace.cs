using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colourful.Colors;

namespace Colourful.RGBWorkingSpaces
{
    public interface IRGBWorkingSpace
    {
        /// <summary>
        /// The companded channel (R, G, B) is made linear with respect to the energy
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        double InverseCompanding(double channel);

        XYZColorBase ReferenceWhite { get; }

        RGBSystemChromacityCoordinates ChromacityCoordinates { get; }
    }
}
