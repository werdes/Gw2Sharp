namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 WvW endpoint.
    /// </summary>
    public interface IWvwClient
    {
        /// <summary>
        /// Gets the WvW abilities.
        /// </summary>
        IWvwAbilitiesClient Abilities { get; }
    }
}
