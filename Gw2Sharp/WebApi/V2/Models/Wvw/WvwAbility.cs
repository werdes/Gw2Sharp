using System;
using System.Collections.Generic;

namespace Gw2Sharp.WebApi.V2.Models
{
    /// <summary>
    /// Represents a WvW ability.
    /// </summary>
    public class WvwAbility : ApiV2BaseObject, IIdentifiable<int>
    {
        /// <summary>
        /// The WvW ability id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The WvW ability name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The WvW ability description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The WvW ability icon.
        /// </summary>
        public RenderUrl Icon { get; set; }

        /// <summary>
        /// The WvW ability ranks.
        /// </summary>
        public IReadOnlyList<WvwAbilityRank> Ranks { get; set; } = Array.Empty<WvwAbilityRank>();
    }
}
