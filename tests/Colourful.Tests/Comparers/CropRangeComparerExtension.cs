using System;
using System.Collections.Generic;

namespace Colourful.Tests
{
    public static class CropRangeComparerExtension
    {
        private class CropRangeEqualityComparer : IEqualityComparer<double>
        {
            private readonly IEqualityComparer<double> _innerEqualityComparer;
            private readonly double _min;
            private readonly double _max;

            public CropRangeEqualityComparer(IEqualityComparer<double> innerEqualityComparer, double min, double max)
            {
                _innerEqualityComparer = innerEqualityComparer;
                _min = min;
                _max = max;
            }

            public bool Equals(double x, double y)
            {
                if (x < _min)
                {
                    x = _min;
                }
                else if (x > _max)
                {
                    x = _max;
                }

                if (y < _min)
                {
                    y = _min;
                }
                else if (y > _max)
                {
                    y = _max;
                }

                return _innerEqualityComparer.Equals(x, y);
            }

            public int GetHashCode(double x)
            {
                if (x < _min)
                {
                    x = _min;
                }
                else if (x > _max)
                {
                    x = _max;
                }

                return _innerEqualityComparer.GetHashCode(x);
            }
        }

        private class CropRangeComparer : IComparer<double>
        {
            private readonly IComparer<double> _innerComparer;
            private readonly double _min;
            private readonly double _max;

            public CropRangeComparer(IComparer<double> innerComparer, double min, double max)
            {
                _innerComparer = innerComparer;
                _min = min;
                _max = max;
            }

            public int Compare(double x, double y)
            {
                if (x < _min)
                {
                    x = _min;
                }
                else if (x > _max)
                {
                    x = _max;
                }

                if (y < _min)
                {
                    y = _min;
                }
                else if (y > _max)
                {
                    y = _max;
                }

                return _innerComparer.Compare(x, y);
            }
        }

        public static IEqualityComparer<double> CropRange(this IEqualityComparer<double> comparer, double min, double max)
            => new CropRangeEqualityComparer(comparer, min, max);

        public static IComparer<double> CropRange(this IComparer<double> comparer, double min, double max)
            => new CropRangeComparer(comparer, min, max);
    }
}