namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents the WvW match map bonus.
    /// </summary>
    public class WvwMatchMapBonus
    {
        /// <summary>
        /// The WvW match map bonus type.
        /// </summary>
        public ApiEnum<WvwMatchMapBonusType> Type { get; set; } = new ApiEnum<WvwMatchMapBonusType>();

        /// <summary>
        /// The WvW match map bonus owner.
        /// </summary>
        public ApiEnum<WvwOwner> Owner { get; set; } = new ApiEnum<WvwOwner>();
    }
}
