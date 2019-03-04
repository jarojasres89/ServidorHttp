using ServidorHttp.Modelo;
using System;
using System.Text;

namespace ServidorHttp.Servicios.CreadorRespuesta
{
    public class CreadorRespuesta200 : ICreadorRespuesta
    {
        public const string MSG_DIR = "/root/msg/";
        public const string WEB_DIR = "/root/web/";
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "Marcos&PatronesTeam 1.1";

        public Respuesta CrearRespuesta(Solicitud solicitud)
        {
            var message = "--------Petición------ \r\n\r\n";

            message += solicitud.Semilla;
            message += "------------------Respuesta------------------\r\n\r\n OK! :D";
            
            var bytes = Encoding.ASCII.GetBytes(message);

            var encabezados = ObtenerEncabezados();
            
            return new Respuesta("200 OK", "text/plain", bytes, encabezados, VERSION, NAME);
        }

        private Encabezado[] ObtenerEncabezados()
        {
            return new Encabezado[] 
            {
                new Encabezado("Expires", "-1"),
                new Encabezado("Date", DateTime.Now.ToLongDateString())
            };
        }
    }
}
