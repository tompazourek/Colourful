using Xunit;

namespace Colourful.Tests.Docs
{
    public class TopicColorDifference
    {
        [Fact]
        public void ComputingColorDifference()
        {
            // CIE Delta-E 1976
            {
                var differenceCalculator = new CIE76ColorDifference();
                var labColor1 = new LabColor(55, 80, 50);
                var labColor2 = new LabColor(18, 36, -60);
                double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 124.1169

                Assert.Equal(124.11688039908189, difference);
            }

            // CMC l:c 1984
            {
                var differenceThreshold = CMCColorDifferenceThreshold.Acceptability; // or "Imperceptibility"
                var differenceCalculator = new CMCColorDifference(differenceThreshold);
                var labColor1 = new LabColor(55, 80, 50);
                var labColor2 = new LabColor(18, 36, -60);
                double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 69.7388

                Assert.Equal(69.7387994508022, difference);
            }

            // CIE Delta-E 1994
            {
                var application = CIE94ColorDifferenceApplication.GraphicArts; // or "Textiles"
                var differenceCalculator = new CIE94ColorDifference(application);
                var labColor1 = new LabColor(55, 80, 50);
                var labColor2 = new LabColor(18, 36, -60);
                double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 60.7882

                Assert.Equal(60.78823549707133, difference);
            }
            
            // CIE Delta-E 2000
            {
                var differenceCalculator = new CIEDE2000ColorDifference();
                var labColor1 = new LabColor(55, 80, 50);
                var labColor2 = new LabColor(18, 36, -60);
                double difference = differenceCalculator.ComputeDifference(in labColor1, in labColor2); // 52.2320

                Assert.Equal(52.23202852556586, difference);
            }
            
            // Delta Ez
            {
                var differenceCalculator = new JzCzhzDEzColorDifference();
                var color1 = new JzCzhzColor(0.3, 0.4, 165);
                var color2 = new JzCzhzColor(0.8, 0.6, 25);
                double difference = differenceCalculator.ComputeDifference(in color1, in color2); // 1.0666

                Assert.Equal(1.066630832433185, difference);
            }
            
            // Euclidean distance
            {
                // example for euclidean distance in the XYZ color space
                var differenceCalculator = new EuclideanDistanceColorDifference<XYZColor>();
                var color1 = new XYZColor(0.5, 0.5, 0.5);
                var color2 = new XYZColor(0.2, 0.4, 0.6);
                double difference = differenceCalculator.ComputeDifference(in color1, in color2); // 0.3317

                Assert.Equal(0.33166247903553997, difference);
            }
        }
    }
}
