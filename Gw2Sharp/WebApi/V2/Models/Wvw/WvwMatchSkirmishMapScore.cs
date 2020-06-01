namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW match skirmish map score.
    /// </summary>
    public class WvwMatchSkirmishMapScore
    {
        /// <summary>
        /// The WvW match skirmish map type.
        /// </summary>
        public ApiEnum<WvwMapType> Type { get; set; } = new ApiEnum<WvwMapType>();

        /// <summary>
        /// The WvW match skirmish map scores.
        /// </summary>
        public WvwTeamValues Scores { get; set; } = new WvwTeamValues();
    }
}
