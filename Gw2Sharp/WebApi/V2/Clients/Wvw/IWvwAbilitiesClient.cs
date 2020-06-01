
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 WvW abilities endpoint.
    /// </summary>
    public interface IWvwAbilitiesClient :
        IAllExpandableClient<WvwAbility>,
        IBulkExpandableClient<WvwAbility, int>,
        ILocalizedClient,
        IPaginatedClient<WvwAbility>
    {
    }
}
