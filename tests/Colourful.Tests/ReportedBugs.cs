using Xunit;

namespace Colourful.Tests
{
    public class ReportedBugs
    {
        [Fact]
        public void Issue48_NullReferenceExceptionOnDefault()
        {
            var defaultColor = default(LinearRGBColor);
            var converter = new ConverterBuilder()
                .FromLinearRGB()
                .ToLinearRGB()
                .Build();

            // ReSharper disable once UnusedVariable
            var actual = converter.Convert(in defaultColor);
        }
    }
}