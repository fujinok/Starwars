using Newtonsoft.Json;
using Starwars.Interfaces;
using Starwars.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starwars.Service
{
    /// <summary>
    /// Star Wars Ship Service.
    /// </summary>
    /// <seealso cref="Starwars.Interfaces.IStarWarsShipService" />
    public class StarWarsShipService : IStarWarsShipService
    {
        /// <summary>
        /// The hour.
        /// </summary>
        private int Hour;

        /// <summary>
        /// The day.
        /// </summary>
        private int Day;

        /// <summary>
        /// The week.
        /// </summary>
        private int Week;

        /// <summary>
        /// The month.
        /// </summary>
        private int Month;

        /// <summary>
        /// The year.
        /// </summary>
        private int Year;

        /// <summary>
        /// The current number of stops.
        /// </summary>
        private int TimeBeforeStopNumber;

        /// <summary>
        /// The current stop time unit.
        /// </summary>
        private int TimeBeforeStopUnit;

        /// <summary>
        /// The HTTP request.
        /// </summary>
        private IHttpRequest HttpRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="StarWarsShipService"/> class.
        /// </summary>
        public StarWarsShipService()
        {
            HttpRequest = new HttpRequest();
            SetValues();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StarWarsShipService"/> class.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        public StarWarsShipService(IHttpRequest httpRequest)
        {
            HttpRequest = httpRequest;
            SetValues();
        }

        /// <summary>
        /// Sets the global values.
        /// </summary>
        private void SetValues()
        {
            Hour = 1;
            Day = 24;
            Week = 7 * Day;
            Month = 30 * Day;
            Year = 12 * Month;
        }

        /// <summary>
        /// Gets all star wars ships.
        /// </summary>
        /// <param name="travelDistance">The travel distance.</param>
        /// <returns>A string of all star ships and the number of stops.</returns>
        public string GetAllStarWarsShips(string travelDistance)
        {
            long targetDistance = long.Parse(travelDistance);

            try
            {
                ICollection<Ship> starWarsShips = AllStarWarsShips();
                return ArrangeShips(starWarsShips, targetDistance);
            }
            catch(Exception ex)
            {
                return ex.Message;
            } 
        }

        /// <summary>
        /// Alls the star wars ships.
        /// </summary>
        /// <returns>Collection of Ship Models</returns>
        public ICollection<Ship> AllStarWarsShips()
        {
            ICollection<Ship> shipList = new List<Ship>();
            StarWarsShips contentResponse;

            contentResponse = StarWarsShipsHttpRequest($"starships/");

            foreach (Ship ship in contentResponse.Results)
            {
                shipList.Add(ship);
            }
            // Check if we have a next page
            while (!string.IsNullOrEmpty(contentResponse.Next))
            {
                // create the correct url
                contentResponse = StarWarsShipsHttpRequest($"starships/{ contentResponse.Next.Substring(contentResponse.Next.IndexOf('?'))}");
                foreach (Ship ship in contentResponse.Results)
                {
                    shipList.Add(ship);
                }
            }
            return shipList;
        }

        /// <summary>
        /// HTTP request.
        /// </summary>
        /// <param name="uri">The URI to call.</param>
        /// <returns>StarWarsShips Model.</returns>
        private StarWarsShips StarWarsShipsHttpRequest(string uri)
        {
            StarWarsShips contentResponse = HttpRequest.CallEndpoint(uri);

            return contentResponse;
        }

        /// <summary>
        /// Arranges the ships.
        /// </summary>
        /// <param name="starWarsShips">The star wars ships.</param>
        /// <param name="targetDistance">The target distance.</param>
        /// <returns>String with a list of ships and number of stops.</returns>
        private string ArrangeShips(ICollection<Ship> starWarsShips, long targetDistance)
        {
            string returnOutput = "";

            foreach (Ship ship in starWarsShips)
            {
                if (!(ship.Consumables.ToLower().Equals("unknown") || ship.MGLT.ToLower().Equals("unknown")))
                {
                    TimeBeforeStop(ship.Consumables);
                    long timeBeforeStop = (TimeBeforeStopUnit * TimeBeforeStopNumber) * int.Parse(ship.MGLT);

                    long numberOfStops = targetDistance / timeBeforeStop;

                    // if the last stop will be a refuel stop it should not be counted.
                    if ((targetDistance % ((TimeBeforeStopUnit * TimeBeforeStopNumber) * int.Parse(ship.MGLT))) == 0)
                    {
                        numberOfStops--;
                    }

                    returnOutput = returnOutput + ship.Name + ": " + numberOfStops + "\n";
                }
                else
                {
                    // Some ships do not have a Consumables or MGLT. If this is the case we return the below string.
                    returnOutput = returnOutput + ship.Name + ": " + $"Consumables: {ship.Consumables} | MGLT: {ship.MGLT}" + "\n";
                }
            }
            return returnOutput;
        }

        /// <summary>
        /// Times the before stop.
        /// Used to see what unit of measure and how many units of measure,
        /// </summary>
        /// <param name="consumables">A string representing how long consumables will last.</param>
        private void TimeBeforeStop(string consumables)
        {
            TimeBeforeStopNumber = int.Parse(consumables.Substring(0, consumables.IndexOf(" ")));
            if (consumables.ToLower().IndexOf("hour") > 0)
            {
                TimeBeforeStopUnit = Hour;
            }
            if (consumables.ToLower().IndexOf("day") > 0)
            {
                TimeBeforeStopUnit = Day;
            }
            else if (consumables.ToLower().IndexOf("week") > 0)
            {
                TimeBeforeStopUnit = Week;
            }
            else if (consumables.ToLower().IndexOf("month") > 0)
            {
                TimeBeforeStopUnit = Month;
            }
            else
            {
                TimeBeforeStopUnit = Year;
            }
        }
    }
}
