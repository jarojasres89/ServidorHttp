using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace ServidorHttp.IntegrationTest
{
    public class HttpServerIntegrationTest
    {
        private readonly int Puerto = 8010;
        Servidor servidor = new Servidor();

        private void StartServer()
        {
            if (servidor.Estado == Enums.EstadosServidor.Detenido)
                servidor.Iniciar(Puerto);
        }

        private void StopServer()
        {
            servidor.Detener();
        }

        private HttpClient CreateClient()
        {
            return new HttpClient();
        }

        private HttpRequestMessage CreateRequest()
        {
            string url = "http://localhost:" + Puerto;
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
            StartServer();

            var client = CreateClient();
            var request = CreateRequest();
            
            using (var response = await client.GetAsync(request.RequestUri))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer();
        }

        [Fact]
        public async void Post()
        {
            StartServer();

            var client = CreateClient();
            var request = CreateRequest();

            var contentJson = "{\"Nombre\": \"Marcos y patrones\",\"Ubicación\": \"Universidad EAFIT\"}";

            var content = new StringContent(contentJson);

            using (var response = await client.PostAsync(request.RequestUri, content))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer();
        }

        [Fact]
        public async void Put()
        {
            StartServer();

            var client = CreateClient();
            var request = CreateRequest();

            var contentJson = "{\"Nombre\": \"Marcos y patrones\",\"Ubicación\": \"Universidad EAFIT\"}";

            var content = new StringContent(contentJson);

            using (var response = await client.PutAsync(request.RequestUri, content))
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Marcos&PatronesTeam 1.1", response.Headers.Server.ToString());
            }
            StopServer();
        }
    }
}
