using Newtonsoft.Json;
using Starwars.Interfaces;
using Starwars.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starwars
{
    /// <summary>
    /// Http Request Class.
    /// </summary>
    /// <seealso cref="Starwars.Interfaces.IHttpRequest" />
    public class HttpRequest : IHttpRequest
    {
        /// <summary>
        /// The target Uri.
        /// </summary>
        private readonly Uri Uri;

        /// <summary>
        /// The response time out as a TimeSpan instance.
        /// </summary>
        private TimeSpan Timeout { get; set; } = new TimeSpan(0, 1, 40);

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequest"/> class.
        /// </summary>
        public HttpRequest()
        {
            Uri = new Uri($"https://swapi.co/api/");
        }

        ///// <summary>
        ///// Alls the star wars ships helper.
        ///// </summary>
        ///// <param name="uri">The URI.</param>
        ///// <returns>StringBuilder object with content of response.</returns>
        ///// <exception cref="System.Exception">Failure to GET from URI: "
        /////                                             + uri
        /////                                             + Uri
        /////                                             + $"\nHttp Response Code is : {response.StatusCode}</exception>
        public StarWarsShips CallEndpoint(string uri)
        {
            StarWarsShips contentResponse = new StarWarsShips();
            var contentStringBuilder = new StringBuilder();
            using (var httpClientHandler = new HttpClientHandler())
            {
                using (var httpClient = new HttpClient(httpClientHandler, true))
                {
                    httpClient.Timeout = Timeout;
                    httpClient.BaseAddress = Uri;

                    HttpResponseMessage response = null;

                    // Make Get call to API 
                    response = Task.Run(() =>
                    {
                        return httpClient.GetAsync($"{uri}");
                    }).Result;

                    // the response has a status code OK  
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader contentStream = new StreamReader(Task.Run(() =>
                        {
                            return response.Content.ReadAsStreamAsync();
                        }).Result))
                        {
                            contentStringBuilder.Append(contentStream.ReadToEndAsync().Result);
                            contentStream.Dispose();
                            contentResponse = JsonConvert.DeserializeObject<StarWarsShips>(contentStringBuilder.ToString());
                        }
                    }
                    // response.StatusCode != HttpStatusCode.OK. This means we had a error in the http call
                    else
                    {
                        throw new Exception($"Failure to GET from URI: "
                                            + Uri
                                            + uri
                                            + $"\nHttp Response Code is : {response.StatusCode}");
                    }

                    return contentResponse;
                }
            }
        }
    }
}
