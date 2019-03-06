using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServidorHttp.Test
{
    [TestClass]
    public class Peticiones
    {
        [TestMethod]
        public async Task Get()
        {

            var config = new HttpConfiguration();
            //configure web api
            config.MapHttpAttributeRoutes();

            using (var server = new HttpServer(config))
            {

                var client = new HttpClient(server);

                string url = "http://localhost:8010/api/product/hello/";

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.SendAsync(request))
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}
