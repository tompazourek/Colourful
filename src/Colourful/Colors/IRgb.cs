namespace Colourful
{
    /// <summary>
    /// An interface implemented by RGB color types.
    /// </summary>
    public interface IRGB
    {
        /// <summary>
        /// The red component.
        /// </summary>
        double R { get; }

        /// <summary>
        /// The green component.
        /// </summary>
        double G { get; }

        /// <summary>
        /// The blue component.
        /// </summary>
        double B { get; }
    }
}