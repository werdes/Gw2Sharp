using System;
using System.Collections.Generic;
using Gw2Sharp.WebApi.V2.Clients;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a guild rank.
    /// </summary>
    public class GuildRank
    {
        /// <summary>
        /// The guild rank id.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The guild rank order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The guild rank permissions.
        /// Each element can be resolved against <see cref="IGuildClient.Permissions"/>.
        /// </summary>
        public IReadOnlyList<string> Permissions { get; set; } = Array.Empty<string>();

        /// <summary>
        /// The guild rank icon.
        /// </summary>
        public RenderUrl Icon { get; set; }
    }
}
