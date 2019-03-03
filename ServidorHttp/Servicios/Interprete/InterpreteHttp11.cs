using ServidorHttp.Modelo;
using System;
using System.IO;
using System.Net.Sockets;

namespace ServidorHttp.Servicios.Interprete
{
    public class InterpreteHttp11 : IInterpreteHttp
    {
        public Solicitud InterpretarSolicitud(object valor)
        {
            if (valor is string solicitud)
            {
                var tokens = solicitud.Split(' ');
                var type = tokens[0];
                var url = tokens[1];
                var host = tokens[4];

                return new Solicitud(solicitud, Enums.VerbosHttp.GET, url, null, type, null);
            }
            return null;
        }

        public void EscribirRespuesta(Respuesta respuesta, TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            var writer = new StreamWriter(stream);

            writer.WriteLine(respuesta.Mensaje);
            writer.Flush();

            stream.Write(respuesta.Contenido, 0, respuesta.Contenido.Length);
            stream.Close();
        }
    }
}
