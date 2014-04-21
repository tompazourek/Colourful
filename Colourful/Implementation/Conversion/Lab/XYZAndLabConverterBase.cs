using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Implementation.Conversion
{
    public abstract class XYZAndLabConverterBase
    {
        protected const double Epsilon = 216d / 24389d;
        protected const double Kappa = 24389d / 27d;
    }
}