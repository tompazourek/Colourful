namespace Colourful;

/// <summary>
/// Weighting parameters for CMC l:c color difference formula.
/// <see cref="CMCColorDifference" /> for usage.
/// </summary>
public enum CMCColorDifferenceThreshold
{
    /// <summary>
    /// 2:1 (l:c).
    /// </summary>
    Acceptability,

    /// <summary>
    /// 1:1 (l:c).
    /// </summary>
    Imperceptibility,
}
