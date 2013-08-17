using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Conversion
{
    interface IColorConverter<in TInput, out TOutput>
    {
        TOutput Convert(TInput input);
    }
}
