namespace Starwars.Interfaces
{
    /// <summary>
    /// Star Wars Ship Service Interface.
    /// </summary>
    public interface IStarWarsShipService
    {
        /// <summary>
        /// Gets all star wars ships.
        /// </summary>
        /// <param name="travelDistance">The travel distance.</param>
        /// <returns>A string of all star ships and the number of stops.</returns>
        string GetAllStarWarsShips(string travelDistance);
    }
}
