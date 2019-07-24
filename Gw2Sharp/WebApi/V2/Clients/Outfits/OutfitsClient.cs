using System;
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 outfits endpoint.
    /// </summary>
    [EndpointPath("outfits")]
    public class OutfitsClient : BaseEndpointBulkAllClient<Outfit, int>, IOutfitsClient
    {
        /// <summary>
        /// Creates a new <see cref="OutfitsClient"/> that is used for the API v2 outfits endpoint.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <param name="gw2Client">The Guild Wars 2 client.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <c>null</c>.</exception>
        protected internal OutfitsClient(IConnection connection, IGw2Client gw2Client) :
            base(connection, gw2Client)
        { }
    }
}
