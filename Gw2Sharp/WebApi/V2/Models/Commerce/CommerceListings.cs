using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents the commerce listings of an item.
    /// </summary>
    public class CommerceListings : ApiV2BaseObject, IIdentifiable<int>
    {
        /// <summary>
        /// The item id.
        /// Can be resolved against <see cref="IGw2WebApiV2Client.Items"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The buy listings.
        /// </summary>
        public IReadOnlyList<CommerceListing> Buys { get; set; } = Array.Empty<CommerceListing>();

        /// <summary>
        /// The sell listings.
        /// </summary>
        public IReadOnlyList<CommerceListing> Sells { get; set; } = Array.Empty<CommerceListing>();
    }
}
