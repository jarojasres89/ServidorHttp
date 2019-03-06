using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace ServidorHttp.IntegrationTest
{
    public class HttpServerIntegrationTest
    {
        private Servidor StartServer(int puerto)
        {
            Servidor servidor = new Servidor();
            servidor.Iniciar(puerto);
            return servidor;
        }

        private void StopServer(Servidor servidor)
        {
            servidor.Detener();
        }

        private HttpClient CreateClient()
        {
            return new HttpClient();
        }

        private HttpRequestMessage CreateRequest(int puerto)
        {
            string url = "http://localhost:" + puerto;
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return request;
        }

        [Fact]
        public async void Get()
        {
            int puerto = 8030;
            var servidor = StartServer(puerto);
            
            var client = CreateClient();
            var request = CreateRequest(puerto);
            
            using (var response = await client.GetAsync(request.RequestUri))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer(servidor);
        }

        [Fact]
        public async void Post()
        {
            int puerto = 8040;
            var servidor = StartServer(puerto);

            var client = CreateClient();
            var request = CreateRequest(puerto);

            var contentJson = "{\"Nombre\": \"Marcos y patrones\",\"Ubicación\": \"Universidad EAFIT\"}";

            var content = new StringContent(contentJson);

            using (var response = await client.PostAsync(request.RequestUri, content))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer(servidor);
        }

        [Fact]
        public async void Put()
        {
            int puerto = 8050;
            var servidor = StartServer(puerto);

            var client = CreateClient();
            var request = CreateRequest(puerto);

            var contentJson = "{\"Nombre\": \"Marcos y patrones\",\"Ubicación\": \"Universidad EAFIT\"}";

            var content = new StringContent(contentJson);

            using (var response = await client.PutAsync(request.RequestUri, content))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer(servidor);
        }
    }
}
