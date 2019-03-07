using ServidorHttp.Enums;
using ServidorHttp.Modelo;
using System;
using System.Text;

namespace ServidorHttp.Servicios.CreadorRespuesta
{
    public class CreadorRespuesta : ICreadorRespuesta
    {        
        public const string VERSION = "HTTP/1.1";
        public const string NAME = "Marcos&PatronesTeam 1.1";

        public Respuesta CrearRespuesta(Solicitud solicitud)
        {
            Respuesta respuesta = null;

            switch (solicitud.Verbo)
            {
                case VerbosHttp.GET:
                    respuesta = CrearRespuestaGET(solicitud);
                    break;
                case VerbosHttp.POST:
                    respuesta = CrearRespuestaPOST(solicitud);
                    break;
                case VerbosHttp.PUT:
                    respuesta = CrearRespuestaPUT(solicitud);
                    break;
                case VerbosHttp.DELETE:
                    respuesta = CrearRespuestaDELETE(solicitud);
                    break;
                case VerbosHttp.OPTIONS:
                    respuesta = CrearRespuestaOPTIONS(solicitud);
                    break;
                case VerbosHttp.HEAD:
                    respuesta = CrearRespuestaHEAD(solicitud);
                    break;
                
                default:
                    respuesta = CrearRespuestaIncorrecta(solicitud);
                    break;
            }

            return respuesta;
        }

        public Respuesta CrearRespuestaGET(Solicitud solicitud)
        {
            if (!string.IsNullOrEmpty(solicitud.Contenido))
            {
                return CrearRespuestaIncorrecta(solicitud);
            }

            var mensaje = "--------Petición------ \r\n\r\n";
            mensaje += solicitud.Semilla;
            mensaje += "------------------Respuesta------------------\r\n\r\n OK! :D";
            var encabezados = ObtenerEncabezadosGenerales();

            return CrearRespuestaCorrecta(mensaje, encabezados);

        }
        public Respuesta CrearRespuestaPOST(Solicitud solicitud)
        {
            var mensaje = "--------Petición------ \r\n\r\n";
            mensaje += solicitud.Semilla;
            mensaje += "------------------Respuesta------------------\r\n\r\n OK! :D";
            var encabezados = ObtenerEncabezadosGenerales();

            return CrearRespuestaCorrecta(mensaje, encabezados);
        }
        public Respuesta CrearRespuestaPUT(Solicitud solicitud)
        {
            var mensaje = "--------Petición------ \r\n\r\n";
            mensaje += solicitud.Semilla;
            mensaje += "------------------Respuesta------------------\r\n\r\n OK! :D";
            var encabezados = ObtenerEncabezadosGenerales();

            return CrearRespuestaCorrecta(mensaje, encabezados);
        }
        
        public Respuesta CrearRespuestaDELETE(Solicitud solicitud)
        {
            var encabezados = ObtenerEncabezadosGenerales();
            return CrearRespuestaCorrecta(string.Empty, encabezados);
        }
        
        public Respuesta CrearRespuestaOPTIONS(Solicitud solicitud) {

            var encabezados = new Encabezado[]
            {
                new Encabezado("Expires", DateTime.Now.AddDays(7).ToLongDateString()),
                new Encabezado("Date", DateTime.Now.ToLongDateString()),
                new Encabezado("Allow", string.Join(",", Enum.GetNames(typeof(VerbosHttp))))
            };
            
            return CrearRespuestaCorrecta(string.Empty, encabezados);

        }
        public Respuesta CrearRespuestaHEAD(Solicitud solicitud)
        {

            if (!string.IsNullOrEmpty(solicitud.Contenido))
            {
                return CrearRespuestaIncorrecta(solicitud);
            }
            
            var encabezados = ObtenerEncabezadosGenerales();
            return CrearRespuestaCorrecta(string.Empty, encabezados);
        }
        
        public Respuesta CrearRespuestaIncorrecta(Solicitud solicitud)
        {
            return new Respuesta("400 Bad Request", "text/plain", new byte[0], new Encabezado[0], VERSION, NAME);
        }

        public Respuesta CrearRespuestaCorrecta(string mensaje, Encabezado[] encabezados)
        {          

            var bytes = Encoding.ASCII.GetBytes(mensaje);


            return new Respuesta("200 OK", "text/plain", bytes, encabezados, VERSION, NAME);
        }
        private Encabezado[] ObtenerEncabezadosGenerales()
        {
            return new Encabezado[] 
            {
                new Encabezado("Expires", DateTime.Now.AddDays(7).ToLongDateString()),
                new Encabezado("Date", DateTime.Now.ToLongDateString())
            };
        }
    }
}
