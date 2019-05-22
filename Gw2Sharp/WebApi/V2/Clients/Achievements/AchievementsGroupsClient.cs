using System;
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 achievements groups endpoint.
    /// </summary>
    [EndpointPath("achievements/groups")]
    public class AchievementsGroupsClient : BaseEndpointBulkAllClient<AchievementGroup, Guid>, IAchievementsGroupsClient
    {
        /// <summary>
        /// Creates a new <see cref="AchievementsGroupsClient"/> that is used for the API v2 achievements groups endpoint.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <c>null</c>.</exception>
        public AchievementsGroupsClient(IConnection connection) :
            base(connection)
        { }
    }
}