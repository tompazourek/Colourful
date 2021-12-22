using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Colourful.Tests.Docs;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class TopicIlluminants
{
    [Fact]
    public void ChromaticityAndXySpace()
    {
        var converter = new ConverterBuilder().Fromxy().ToXYZ().Build();
        var d93Chromaticity = new xyChromaticity(0.28315, 0.29711);
        var d93WhitePoint = converter.Convert(d93Chromaticity); // XYZ [X=0.95, Y=1, Z=1.41]

        Assert.Equal(0.95301403520581607, d93WhitePoint.X);
        Assert.Equal(1, d93WhitePoint.Y);
        Assert.Equal(1.4127427552085088, d93WhitePoint.Z);
    }
}
