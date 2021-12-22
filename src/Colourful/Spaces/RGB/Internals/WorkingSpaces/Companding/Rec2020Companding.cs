using System;
using System.Diagnostics.CodeAnalysis;
using static System.Math;

namespace Colourful.Internals;

/// <summary>
/// Rec. 2020 companding function (for 12-bit).
/// </summary>
/// <remarks>
/// http://en.wikipedia.org/wiki/Rec._2020
/// For 10-bits, companding is identical to <see cref="Rec709Companding" />
/// </remarks>
public class Rec2020Companding : ICompanding, IEquatable<Rec2020Companding>
{
    private const double Alpha = 1.09929682680944;
    private const double Beta = 0.018053968510807;
    private const double InverseBeta = Beta * 4.5;

    /// <inheritdoc />
    public double ConvertToLinear(in double nonLinearChannel)
    {
        var V = nonLinearChannel;
        var L = V < InverseBeta ? V / 4.5 : Pow((V + Alpha - 1.0) / Alpha, 1 / 0.45);
        return L;
    }

    /// <inheritdoc />
    public double ConvertToNonLinear(in double linearChannel)
    {
        var L = linearChannel;
        var V = L < Beta ? 4.5 * L : Alpha * Pow(L, y: 0.45) - (Alpha - 1.0);
        return V;
    }

    #region Equality

    /// <inheritdoc />
    public bool Equals(Rec2020Companding other)
    {
        if (other == null)
            return false;

        return true;
    }

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Rec2020Companding;

    /// <inheritdoc />
    public override int GetHashCode() => typeof(Rec2020Companding).GetHashCode();

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator ==(Rec2020Companding left, Rec2020Companding right) => Equals(left, right);

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator !=(Rec2020Companding left, Rec2020Companding right) => !Equals(left, right);

    #endregion
}
