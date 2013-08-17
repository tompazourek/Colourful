using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Conversion
{
    internal interface IColorConverter<in TInput, out TOutput>
    {
        TOutput Convert(TInput input);
    }
}