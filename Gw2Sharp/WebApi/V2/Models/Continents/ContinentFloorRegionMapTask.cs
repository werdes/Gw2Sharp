using System;
using System.Collections.Generic;
using Gw2Sharp.Models;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a continent floor region map task.
    /// </summary>
    public class ContinentFloorRegionMapTask : ApiV2BaseObject, IIdentifiable<int>
    {
        /// <summary>
        /// The task id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The task objective.
        /// </summary>
        public string Objective { get; set; } = string.Empty;

        /// <summary>
        /// The task level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The task coordinates.
        /// </summary>
        public Coordinates2 Coord { get; set; }

        /// <summary>
        /// The task bounds.
        /// </summary>
        public IReadOnlyList<Coordinates2> Bounds { get; set; } = Array.Empty<Coordinates2>();

        /// <summary>
        /// The sector chat link.
        /// </summary>
        public string ChatLink { get; set; } = string.Empty;
    }
}
