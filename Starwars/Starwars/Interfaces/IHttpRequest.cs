using Starwars.Models;
using System.Text;

namespace Starwars.Interfaces
{
    /// <summary>
    /// Http Request Interface.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Calls the endpoint provided.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>StarWarsShip Model</returns>
        StarWarsShips CallEndpoint(string uri);
    }
}
       