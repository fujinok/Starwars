using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Starwars.Models;
using System.Collections.Generic;

namespace Starwars.UnitTests
{
    [TestClass]
    public class StarWarsShipServiceTests : StarWarsTestBase
    {
        /// <summary>
        /// Star
        /// </summary>
        StarWarsShips StarWarsShipsTest;
        StarWarsShips StarWarsShipsTest2;


        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public virtual void Initialize()
        {
          
        }

        /// <summary>
        /// GetAllStarWarsShips should return string when data is valid.
        /// </summary>
        [TestMethod]
        public void GetAllStarWarsShips_ShouldReturnString_When_Data_IsValid()
        {
            StarWarsShipsTest = new StarWarsShips()
            {
                Count = 1,
                Next = null,
                Previous = null,
            };
            StarWarsShipsTest.Results.Add(new Ship()
            {
                Name = "executor",
                Consumables = "1 day",
                MGLT = "100"
            });

            mockHttpRequest.Setup(h => h.CallEndpoint(It.IsAny<string>())).Returns(StarWarsShipsTest);

            var response = StarWarsShipService.GetAllStarWarsShips("24500");

            Assert.IsNotNull(response);

            Assert.AreEqual("executor: 10\n", response);
        }

        /// <summary>
        /// GetAllStarWarsShips should return string when data is valid And Last Stop Is A Refuel.
        /// </summary>
        [TestMethod]
        public void GetAllStarWarsShips_ShouldReturnString_When_Data_IsValid_And_Last_Stop_Is_A_Refuel()
        {
            StarWarsShipsTest = new StarWarsShips()
            {
                Count = 1,
                Next = null,
                Previous = null,
            };
            StarWarsShipsTest.Results.Add(new Ship()
            {
                Name = "executor",
                Consumables = "1 day",
                MGLT = "100"
            });

            mockHttpRequest.Setup(h => h.CallEndpoint(It.IsAny<string>())).Returns(StarWarsShipsTest);

            var response = StarWarsShipService.GetAllStarWarsShips("24000");

            Assert.IsNotNull(response);

            Assert.AreEqual("executor: 9\n", response);
        }


        /// <summary>
        /// GetAllStarWarsShips should return string when data is valid and next has value.
        /// </summary>
        [TestMethod]
        public void GetAllStarWarsShips_ShouldReturnString_When_Data_IsValid_And_Next_Has_Value()
        {
            StarWarsShipsTest = new StarWarsShips()
            {
                Count = 1,
                Next = $"starships/?page=2",
                Previous = null,
            };
            StarWarsShipsTest.Results.Add(new Ship()
            {
                Name = "executor",
                Consumables = "2 days",
                MGLT = "100"
            });

            StarWarsShipsTest2 = new StarWarsShips()
            {
                Count = 1,
                Next = null,
                Previous = null,
            };
            StarWarsShipsTest2.Results.Add(new Ship()
            {
                Name = "deathstar",
                Consumables = "2 years",
                MGLT = "60"
            });

            mockHttpRequest.Setup(h => h.CallEndpoint(It.IsAny <string>())).Returns(StarWarsShipsTest);

            mockHttpRequest.Setup(h => h.CallEndpoint(StarWarsShipsTest.Next)).Returns(StarWarsShipsTest2);

            var response = StarWarsShipService.GetAllStarWarsShips("1000");

            Assert.IsNotNull(response);

            Assert.AreEqual("executor: 0\ndeathstar: 0\n", response);
        }

        /// <summary>
        /// GetAllStarWarsShips should return string when data is valid and Consumables is unknown.
        /// </summary>
        [TestMethod]
        public void GetAllStarWarsShips_ShouldReturnString_When_Data_IsValid_And_Consumables_Is_Unknown()
        {
            StarWarsShipsTest = new StarWarsShips()
            {
                Count = 1,
                Next = null,
                Previous = null,
            };
            StarWarsShipsTest.Results.Add(new Ship()
            {
                Name = "executor",
                Consumables = "Unknown",
                MGLT = "100"
            }); 

            mockHttpRequest.Setup(h => h.CallEndpoint(It.IsAny<string>())).Returns(StarWarsShipsTest);

            var response = StarWarsShipService.GetAllStarWarsShips("1100");

            Assert.IsNotNull(response);

            Assert.AreEqual("executor: Consumables: Unknown | MGLT: 100\n", response);
        }

        /// <summary>
        /// GetAllStarWarsShips should return string when data is valid and MGLT is unknown.
        /// </summary>
        [TestMethod]
        public void GetAllStarWarsShips_ShouldReturnString_When_Data_IsValid_And_MGLT_Is_Unknown()
        {
            StarWarsShipsTest = new StarWarsShips()
            {
                Count = 1,
                Next = null,
                Previous = null,
            };
            StarWarsShipsTest.Results.Add(new Ship()
            {
                Name = "executor",
                Consumables = "1 day",
                MGLT = "unknown"
            });

            mockHttpRequest.Setup(h => h.CallEndpoint(It.IsAny<string>())).Returns(StarWarsShipsTest);

            var response = StarWarsShipService.GetAllStarWarsShips("1100");

            Assert.IsNotNull(response);

            Assert.AreEqual("executor: Consumables: 1 day | MGLT: unknown\n", response);
        }
    }
}
