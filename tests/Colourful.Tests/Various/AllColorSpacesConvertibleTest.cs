using System;
using System.Linq;
using System.Reflection;
using Colourful.Internals;
using Xunit;
using static Colourful.Internals.ConversionMetadataUtils;

namespace Colourful.Tests
{
    public class AllColorSpacesConvertibleTest
    {
        [Fact]
        public void EveryColorSpaceCanBeConvertedToAnyOther_DifferentMetadata()
        {
            var colorSpaces = typeof(IColorSpace).Assembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y == typeof(IColorSpace)))
                .ToList();

            foreach (var sourceColorSpace in colorSpaces)
            {
                foreach (var targetColorSpace in colorSpaces)
                {
                    var testConversionMethodGeneric = typeof(AllColorSpacesConvertibleTest).GetMethod(nameof(TestConversion_DifferentMetadata), BindingFlags.NonPublic | BindingFlags.Static);
                    // ReSharper disable once PossibleNullReferenceException
                    var testConversionMethod = testConversionMethodGeneric.MakeGenericMethod(sourceColorSpace, targetColorSpace);

                    object sourceColor = GetSourceColor(sourceColorSpace);
                    testConversionMethod.Invoke(obj: null, new[] { sourceColor });
                }
            }
        }

        [Fact]
        public void EveryColorSpaceCanBeConvertedToAnyOther_SameMetadata()
        {
            var colorSpaces = typeof(IColorSpace).Assembly
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y == typeof(IColorSpace)))
                .ToList();

            foreach (var sourceColorSpace in colorSpaces)
            {
                foreach (var targetColorSpace in colorSpaces)
                {
                    var testConversionMethodGeneric = typeof(AllColorSpacesConvertibleTest).GetMethod(nameof(TestConversion_SameMetadata), BindingFlags.NonPublic | BindingFlags.Static);
                    // ReSharper disable once PossibleNullReferenceException
                    var testConversionMethod = testConversionMethodGeneric.MakeGenericMethod(sourceColorSpace, targetColorSpace);

                    object sourceColor = GetSourceColor(sourceColorSpace);
                    testConversionMethod.Invoke(obj: null, new[] { sourceColor });
                }
            }
        }

        private static IColorSpace GetSourceColor(Type colorType)
        {
            var constructors = colorType.GetConstructors();
            foreach (var constructor in constructors)
            {
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length != 1)
                    continue;

                var constructorParameter = constructorParameters[0];
                if (constructorParameter.ParameterType != typeof(double[]).MakeByRefType())
                    continue;

                double[] vector = { .51, .49, .5 };
                var result = constructor.Invoke(new object[] { vector });
                return (IColorSpace)result;
            }

            throw new InvalidOperationException($"Cannot create an instance of {colorType} from a vector.");
        }

        private static void TestConversion_DifferentMetadata<TSource, TTarget>(TSource sourceColor)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            var converter = new ConverterBuilder()
                .From<TSource>(new ConversionMetadata(
                    CreateWhitePoint(Illuminants.D55),
                    CreateRGBPrimaries(RGBWorkingSpaces.AdobeRGB1998.Primaries),
                    CreateCompanding(RGBWorkingSpaces.AdobeRGB1998.Companding)
                ))
                .To<TTarget>(new ConversionMetadata(
                    CreateWhitePoint(Illuminants.D65),
                    CreateRGBPrimaries(RGBWorkingSpaces.ApplesRGB.Primaries),
                    CreateCompanding(RGBWorkingSpaces.ApplesRGB.Companding)
                ))
                .Build();

            var targetColor = converter.Convert(in sourceColor);
            Assert.NotNull(targetColor);
        }

        private static void TestConversion_SameMetadata<TSource, TTarget>(TSource sourceColor)
            where TSource : IColorSpace
            where TTarget : IColorSpace
        {
            var conversionMetadata = new ConversionMetadata(
                CreateWhitePoint(RGBWorkingSpaces.sRGB.WhitePoint),
                CreateRGBPrimaries(RGBWorkingSpaces.sRGB.Primaries),
                CreateCompanding(RGBWorkingSpaces.sRGB.Companding)
            );
            var converter = new ConverterBuilder()
                .From<TSource>(conversionMetadata)
                .To<TTarget>(conversionMetadata)
                .Build();

            var targetColor = converter.Convert(in sourceColor);
            Assert.NotNull(targetColor);
        }
    }
}
