using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Colourful.Tests.Assertions
{
    public static class CustomAssert
    {
        [SuppressMessage("ReSharper", "RedundantCast")]
        public static void EqualsWithHashCode<T1, T2>(T1 x, T2 y)
        {
            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [SuppressMessage("ReSharper", "RedundantCast")]
        public static void NotEqualsWithHashCode<T1, T2>(T1 x, T2 y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.NotEqual(x.GetHashCode(), y.GetHashCode());
        }
    }
}