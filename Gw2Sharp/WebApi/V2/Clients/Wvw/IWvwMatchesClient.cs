
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 WvW matches endpoint.
    /// </summary>
    public interface IWvwMatchesClient :
        IAllExpandableClient<WvwMatch>,
        IBulkExpandableClient<WvwMatch, string>,
        IPaginatedClient<WvwMatch>
    {
    }
}
