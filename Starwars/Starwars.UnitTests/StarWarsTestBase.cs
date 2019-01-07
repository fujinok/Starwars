using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Starwars.Interfaces;
using Starwars.Service;

namespace Starwars.UnitTests
{
    /// <summary>
    /// Base Class for Unit Tests
    /// </summary>
    [TestClass]
    public class StarWarsTestBase
    {
        /// <summary>
        /// The mock HTTP request
        /// </summary>
        internal Mock<IHttpRequest> mockHttpRequest;

        /// <summary>
        /// The program service
        /// </summary>
        internal StarWarsShipService StarWarsShipService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StarwarsTestBase"/> class.
        /// </summary>
        public StarWarsTestBase()
        {
            mockHttpRequest = new Mock<IHttpRequest>();
            StarWarsShipService = new StarWarsShipService(mockHttpRequest.Object);
        }
    }
}
