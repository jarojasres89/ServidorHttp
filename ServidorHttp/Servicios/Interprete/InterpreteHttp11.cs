using ServidorHttp.Enums;
using ServidorHttp.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ServidorHttp.Servicios.Interprete
{
    public class InterpreteHttp11 : IInterpreteHttp
    {

        public Solicitud InterpretarSolicitud(TcpClient cliente)
        {
            var textoPeticion = ObtenerMensajeSolicitud(cliente);

            if (textoPeticion == string.Empty)
                return null;

            var lineasPeticion = textoPeticion.Split(new[] { '\r'}).Select( x=> x.Replace("\n", "")).ToArray();            
            var primeraLinea = lineasPeticion[0].Split(new[] { ' ' });
            VerbosHttp verboHttp;

            if (Enum.TryParse(primeraLinea[0], out verboHttp))
            {
                var encabezado = lineasPeticion.Skip(1).TakeWhile(r => r != "").Select(x => new Encabezado(x.Split(':').First(), string.Join(":", x.Split(new[] { ':' }).Skip(1)))).ToArray();

                var host = new Uri(@"http://" + encabezado.First(x => x.Nombre == "Host").Valor.Trim());
                var url = new Uri(host, primeraLinea[1]);
                var tipoContenido = encabezado.FirstOrDefault(x => x.Nombre == "Content-Type")?.Valor;
                var indiceInicioCuerpo = Array.IndexOf(lineasPeticion, string.Empty) + 1;
                var cuerpo = string.Join("", lineasPeticion.Skip(indiceInicioCuerpo).Select(x => x.Replace("\t", string.Empty)));

                return new Solicitud(textoPeticion, verboHttp, url.ToString(), cuerpo, tipoContenido, encabezado);
            }

            return new Solicitud(true);
        }

        private string ObtenerMensajeSolicitud(TcpClient cliente)
        {
            var networkStream = cliente.GetStream();
            var buffer = new byte[cliente.ReceiveBufferSize];

            var bytesRead = networkStream.Read(buffer, 0, cliente.ReceiveBufferSize);

            string textoPeticion = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            return textoPeticion;

        }


        public void EscribirRespuesta(Respuesta respuesta, TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            var writer = new StreamWriter(stream);

            writer.WriteLine(MensajeRespuesta(respuesta));
            writer.Flush();

            stream.Write(respuesta.Contenido, 0, respuesta.Contenido.Length);
            stream.Close();
        }

        public string MensajeRespuesta(Respuesta respuesta)
        {
            var textoEncabezados = ObtenerEncabezados(respuesta.Encabezados);
            return $"{respuesta.VersionServidor} {respuesta.CodigoEstado}\r\nServer: {respuesta.NombreServidor}\r\nContent-Type: {respuesta.TipoContenido}\r\nAccept-Ranges: bytes\r\nContent-Length: {respuesta.Contenido.Length}{textoEncabezados}\r\n";
        }
        
        public string ObtenerEncabezados(Encabezado[] encabezados) =>
            string.Join(
                        string.Empty, textoEncabezados(encabezados).ToArray()
                        );

        private IEnumerable<string> textoEncabezados(Encabezado[] encabezados) =>
            encabezados?.Select(
                        r => $"\r\n{r.Nombre}: {r.Valor}"
                    ) ?? new List<string>();
    }
}
