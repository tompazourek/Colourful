using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.RGBWorkingSpaces
{
    /// <summary>
    /// Companded channel is made linear with respect to the energy.
    /// </summary>
    public interface ICompanding
    {
        /// <summary>
        /// Companded channel is made linear with respect to the energy.
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_RGB_to_XYZ.html
        /// </remarks>
        double InverseCompanding(double channel);

        /// <summary>
        /// Uncompanded channel (linear) is made nonlinear (depends on the RGB color system).
        /// </summary>
        /// <remarks>
        /// For more info see:
        /// http://www.brucelindbloom.com/index.html?Eqn_XYZ_to_RGB.html
        /// </remarks>
        double Companding(double channel);
    }
}
