using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW match.
    /// </summary>
    public class WvwMatch : ApiV2BaseObject, IIdentifiable<string>
    {
        /// <summary>
        /// The WvW match id.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The WvW match start time.
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// The WvW match end time.
        /// </summary>
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// The WvW match scores.
        /// </summary>
        public WvwTeamValues Scores { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match hosting worlds.
        /// </summary>
        public WvwTeamValues Worlds { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match worlds that includes the guesting worlds.
        /// </summary>
        public WvwTeamValuesMulti AllWorlds { get; set; } = new WvwTeamValuesMulti();

        /// <summary>
        /// The WvW match deaths.
        /// </summary>
        public WvwTeamValues Deaths { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match kills.
        /// </summary>
        public WvwTeamValues Kills { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match victory points.
        /// </summary>
        public WvwTeamValues VictoryPoints { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match skirmishes.
        /// </summary>
        public IReadOnlyList<WvwMatchSkirmish> Skirmishes { get; set; } = Array.Empty<WvwMatchSkirmish>();

        /// <summary>
        /// The Wvw match maps.
        /// </summary>
        public IReadOnlyList<WvwMatchMap> Maps { get; set; } = Array.Empty<WvwMatchMap>();
    }
}
