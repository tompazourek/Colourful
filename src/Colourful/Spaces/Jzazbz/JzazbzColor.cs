using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Colourful;

/// <summary>
/// Jz az bz color space introduced in Safdar &amp; al. (2017).
/// See: https://www.osapublishing.org/oe/abstract.cfm?uri=oe-25-13-15131
/// </summary>
[SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "They're immutable, and we don't need getters.")]
public readonly struct JzazbzColor : IColorSpace, IColorVector, IEquatable<JzazbzColor>
{
    #region Constructor

    /// <param name="jz">J_z (lightness) (from 0 to 1).</param>
    /// <param name="az">a_z (redness-greenness) (from -1 to 1).</param>
    /// <param name="bz">b_z (yellowness-blueness) (from -1 to 1).</param>
    public JzazbzColor(in double jz, in double az, in double bz)
    {
        Jz = jz;
        this.az = az;
        this.bz = bz;
    }

    /// <param name="vector"><see cref="Vector" />, expected 3 dimensions.</param>
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not checking this for brevity.")]
    public JzazbzColor(in double[] vector) : this(vector[0], vector[1], vector[2])
    {
    }

    #endregion

    #region Channels

    /// <summary>
    /// J_z (lightness).
    /// Ranges usually between 0 and 1.
    /// </summary>
    public readonly double Jz;

    /// <summary>
    /// a_z (redness-greenness).
    /// Ranges usually between -1 and 1.
    /// </summary>
    public readonly double az;

    /// <summary>
    /// b_z (yellowness-blueness).
    /// Ranges usually between -1 and 1.
    /// </summary>
    public readonly double bz;

    /// <inheritdoc />
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Array for performance reasons.")]
    public double[] Vector => new[] { Jz, az, bz };

    #endregion

    #region Equality

    /// <inheritdoc cref="object" />
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    public bool Equals(JzazbzColor other) =>
        Jz == other.Jz &&
        az == other.az &&
        bz == other.bz;

    /// <inheritdoc cref="object" />
    public override bool Equals(object obj) => obj is JzazbzColor other && Equals(other);

    /// <inheritdoc cref="object" />
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Jz.GetHashCode();
            hashCode = (hashCode * 397) ^ az.GetHashCode();
            hashCode = (hashCode * 397) ^ bz.GetHashCode();
            return hashCode;
        }
    }

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator ==(JzazbzColor left, JzazbzColor right) => Equals(left, right);

    /// <inheritdoc cref="object" />
#if !NETSTANDARD1_1
    [ExcludeFromCodeCoverage]
#endif
    public static bool operator !=(JzazbzColor left, JzazbzColor right) => !Equals(left, right);

    #endregion

    #region Deconstructor

    /// <summary>
    /// Deconstructs color into individual channels.
    /// </summary>
    public void Deconstruct(out double jz, out double az, out double bz)
    {
        jz = Jz;
        az = this.az;
        bz = this.bz;
    }

    #endregion

    #region Overrides

    /// <inheritdoc cref="object" />
    public override string ToString() => string.Format(CultureInfo.InvariantCulture, "Jzazbz [Jz={0:0.##}, az={1:0.##}, bz={2:0.##}]", Jz, az, bz);

    #endregion
}
