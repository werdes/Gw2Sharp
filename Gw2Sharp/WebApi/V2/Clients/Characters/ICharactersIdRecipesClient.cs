using System.Collections.Generic;
using Gw2Sharp.WebApi.V2.Models;

namespace Gw2Sharp.WebApi.V2.Clients
{
    /// <summary>
    /// A client of the Guild Wars 2 API v2 characters id recipes endpoint.
    /// </summary>
    public interface ICharactersIdRecipesClient :
        IAuthenticatedClient,
        IBlobClient<CharactersRecipes>
    {
        /// <summary>
        /// The character name.
        /// </summary>
        string CharacterName { get; }
    }
}
