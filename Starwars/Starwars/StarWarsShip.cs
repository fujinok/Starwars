using Starwars.Service;
using System;

namespace Starwars
{
    /// <summary>
    /// 
    /// </summary>
    class Starwars
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            string finish = "n";
            string travelDistance = "";
            while(finish.ToLower().Equals("n"))
            {
                Console.Write("Please enter the distance for a starship to travel: ");
                travelDistance = Console.ReadLine();

                StarWarsShipService starWarsShipService = new StarWarsShipService();

                Console.WriteLine(starWarsShipService.GetAllStarWarsShips(travelDistance));

                Console.Write($"Do you want to finish 'Y' or 'N': ");
                finish = Console.ReadLine();
            } 
        }
    }
}
