using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW match skirmish.
    /// </summary>
    public class WvwMatchSkirmish
    {
        /// <summary>
        /// The WvW match skirmish id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The WvW match skirmish scores.
        /// </summary>
        public WvwTeamValues Scores { get; set; } = new WvwTeamValues();

        /// <summary>
        /// The WvW match skirmish map scores.
        /// </summary>
        public IReadOnlyList<WvwMatchSkirmishMapScore> MapScores { get; set; } = Array.Empty<WvwMatchSkirmishMapScore>();
    }
}
