using System.Diagnostics.CodeAnalysis;

namespace Colourful
{
    /// <summary>
    /// Identifier of color spaces. Used only for generic constraints.
    /// </summary>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Need this to distinguish color spaces for generics (to avoid mistakes).")]
    public interface IColorSpace
    {
    }
}
