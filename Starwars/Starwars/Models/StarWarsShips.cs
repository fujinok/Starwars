using System.Collections.Generic;

namespace Starwars.Models
{
    /// <summary>
    /// StarWarsShips Model.
    /// </summary>
    public class StarWarsShips
    {
        /// <summary>
        /// Gets or sets count.
        /// </summary>
        /// <value>
        /// The count of ships.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets next.
        /// </summary>
        /// <value>
        /// The next page.
        /// </value>
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets previous.
        /// </summary>
        /// <value>
        /// The previous page.
        /// </value>
        public object Previous { get; set; }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <value>
        /// List of ships returned.
        /// </value>
        public List<Ship> Results { get; } = new List<Ship>();
    }
}
