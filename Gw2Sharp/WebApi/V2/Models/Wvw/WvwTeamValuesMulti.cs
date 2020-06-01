using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents specific values for all WvW teams.
    /// </summary>
    public class WvwTeamValuesMulti
    {
        /// <summary>
        /// The values for the red WvW team.
        /// </summary>
        public IReadOnlyList<int> Red { get; set; } = Array.Empty<int>();

        /// <summary>
        /// The values for the blue WvW team.
        /// </summary>
        public IReadOnlyList<int> Blue { get; set; } = Array.Empty<int>();

        /// <summary>
        /// The values for the green WvW team.
        /// </summary>
        public IReadOnlyList<int> Green { get; set; } = Array.Empty<int>();
    }
}
