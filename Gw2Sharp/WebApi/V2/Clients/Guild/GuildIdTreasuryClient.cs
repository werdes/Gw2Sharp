using System;
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 guild id treasury endpoint.
    /// </summary>
    [EndpointPath("guild/:id/treasury")]
    [EndpointPathSegment("id", 0)]
    public class GuildIdTreasuryClient : BaseEndpointBlobClient<IApiV2ObjectList<GuildTreasuryItem>>, IGuildIdTreasuryClient
    {
        private readonly Guid guildId;

        /// <summary>
        /// Creates a new <see cref="GuildIdTreasuryClient"/> that is used for the API v2 guild id treasury endpoint.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <param name="guildId">The guild id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <c>null</c>.</exception>
        public GuildIdTreasuryClient(IConnection connection, Guid guildId) :
            base(connection)
        {
            this.guildId = guildId;
        }

        /// <inheritdoc />
        public virtual Guid GuildId => this.guildId;
    }
}