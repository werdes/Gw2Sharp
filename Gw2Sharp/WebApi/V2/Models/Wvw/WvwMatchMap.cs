using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW match map.
    /// </summary>
    public class WvwMatchMap
    {
        /// <summary>
        /// The map id.
        /// Can be resolved against <see cref="IGw2WebApiV2Client.Maps"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The WvW match map type.
        /// </summary>
        public ApiEnum<WvwMapType> Type { get; set; } = new ApiEnum<WvwMapType>();

        /// <summary>
        /// The WvW match map scores.
        /// </summary>
        public WvwTeamValues Scores { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match map bonuses.
        /// </summary>
        public IReadOnlyList<WvwMatchMapBonus> Bonuses { get; set; } = Array.Empty<WvwMatchMapBonus>();

        /// <summary>
        /// The WvW match map objectives.
        /// </summary>
        public IReadOnlyList<WvwMatchMapObjective> Objectives { get; set; } = Array.Empty<WvwMatchMapObjective>();

        /// <summary>
        /// The WvW match map deaths.
        /// </summary>
        public WvwTeamValues Deaths { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match map kills.
        /// </summary>
        public WvwTeamValues Kills { get; set; } = new WvwTeamValues();
    }
}
