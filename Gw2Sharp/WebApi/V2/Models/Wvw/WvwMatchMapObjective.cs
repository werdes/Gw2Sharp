using System;
using System.Collections.Generic;
using Gw2Sharp.WebApi.V2.Clients;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents the WvW match map objective.
    /// </summary>
    public class WvwMatchMapObjective
    {
        /// <summary>
        /// The WvW objective id.
        /// Can be resolved against <see cref="IWvwClient.Objectives"/>.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The WvW objective type.
        /// </summary>
        public ApiEnum<WvwObjectiveType> Type { get; set; } = new ApiEnum<WvwObjectiveType>();

        /// <summary>
        /// The WvW objective owner.
        /// </summary>
        public ApiEnum<WvwOwner> Owner { get; set; } = new ApiEnum<WvwOwner>();

        /// <summary>
        /// The time when the WvW objective was last flipped.
        /// </summary>
        public DateTimeOffset LastFlipped { get; set; }

        /// <summary>
        /// The guild that has claimed the WvW objective.
        /// If no guild has claimed the WvW objective, or if the WvW objective can't be claimed, this value is <c>null</c>.
        /// </summary>
        public Guid? ClaimedBy { get; set; }

        /// <summary>
        /// The time when the WvW objective was claimed.
        /// If no guild has claimed the WvW objective, or if the WvW objective can't be claimed, this value is <c>null</c>.
        /// </summary>
        public DateTimeOffset? ClaimedAt { get; set; }

        /// <summary>
        /// The yielded points per tick of the WvW objective.
        /// </summary>
        public int PointsTick { get; set; }

        /// <summary>
        /// The awarded points on capture of the WvW objective.
        /// </summary>
        public int PointsCapture { get; set; }

        /// <summary>
        /// The number of dolyaks that have been delivered to the WvW objective.
        /// If no dolyaks can be delivered to the WvW objective, this value is <c>null</c>.
        /// </summary>
        public int? YaksDelivered { get; set; }

        /// <summary>
        /// The guild upgrades that are currently slotted in the WvW objective.
        /// Each element can be resolved against <see cref="IGuildClient.Upgrades"/>.
        /// If no guild upgrades can be slotted in the WvW objective, this value is <c>null</c>.
        /// </summary>
        public IReadOnlyList<int>? GuildUpgrades { get; set; }
    }
}
