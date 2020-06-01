namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW ability rank.
    /// </summary>
    public class WvwAbilityRank
    {
        /// <summary>
        /// The WvW ability rank cost.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// The WvW ability rank effect.
        /// </summary>
        public string Effect { get; set; } = string.Empty;
    }
}
