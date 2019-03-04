using ServidorHttp.Enums;
using ServidorHttp.Modelo;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace ServidorHttp.Servicios.Interprete
{
    public class InterpreteHttp11 : IInterpreteHttp
    {
        public Solicitud InterpretarSolicitud(object valor)
        {
            var textoPeticion = valor as string;

            if (textoPeticion == string.Empty)
                return null;

            var lineasPeticion = textoPeticion.Split(new[] { '\r', '\n' });
            var primeraLinea = lineasPeticion[0].Split(new[] { ' ' });
            VerbosHttp verboHttp;
            Enum.TryParse(primeraLinea[0], out verboHttp);

            var encabezado = lineasPeticion.Skip(1).TakeWhile(r => r != "").Select(x => new Encabezado(x.Split(':').First(), string.Join(":", x.Split(new[] { ':' }).Skip(1)))).ToArray();

            var host = new Uri(@"http://" + encabezado.First(x => x.Nombre == "Host").Valor.Trim());
            var url = new Uri(host, primeraLinea[1]);
            var tipoContenido = encabezado.FirstOrDefault(x => x.Nombre == "Content-Type")?.Valor;
            var indiceInicioCuerpo = Array.IndexOf(lineasPeticion, string.Empty) + 1;
            var cuerpo = string.Join("", lineasPeticion.Skip(indiceInicioCuerpo).Select(x => x.Replace("\t", string.Empty)));

            return new Solicitud(textoPeticion, verboHttp, url.ToString(), cuerpo, tipoContenido, encabezado);

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
