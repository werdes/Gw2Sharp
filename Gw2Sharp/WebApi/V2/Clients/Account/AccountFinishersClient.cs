using System;
using System.Collections.Generic;
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 account finishers endpoint.
    /// </summary>
    [EndpointPath("account/finishers")]
    public class AccountFinishersClient : BaseEndpointBlobClient<IReadOnlyList<AccountFinisher>>, IAccountFinishersClient
    {
        /// <summary>
        /// Creates a new <see cref="AccountFinishersClient"/> that is used for the API v2 account finishers endpoint.
        /// </summary>
        /// <param name="connection">The connection used to make requests, see <see cref="IConnection"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="connection"/> is <c>null</c>.</exception>
        public AccountFinishersClient(IConnection connection) :
            base(connection)
        { }
    }
}