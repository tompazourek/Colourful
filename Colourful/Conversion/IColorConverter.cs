using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colourful.Conversion
{
    /// <summary>
    /// Converts between two color spaces
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public interface IColorConverter<in TInput, out TOutput>
    {
        TOutput Convert(TInput input);
    }
}