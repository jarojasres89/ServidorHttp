using System.Collections.Generic;
using System.Linq;

namespace ServidorHttp.Modelo
{
    public class Respuesta
    {
        public Respuesta(string codigoEstaddo, string tipoContenido, byte[] contenido, Encabezado[] encabezados, string versionServidor, string nombreServidor)
        {
            CodigoEstado = codigoEstaddo;
            TipoContenido = tipoContenido;
            Contenido = contenido;
            Encabezados = encabezados;
            VersionServidor = versionServidor;
            NombreServidor = nombreServidor;
        }

        public string CodigoEstado { get; }
        public string TipoContenido { get; }
        public byte[] Contenido { get; }
        public Encabezado[] Encabezados { get; }
        public string VersionServidor { get; }
        public string NombreServidor { get; }
        public string Mensaje {
            get
            {
                return $"{VersionServidor} {CodigoEstado}\r\nServer: {NombreServidor}\r\nContent-Type: {TipoContenido}\r\nAccept-Ranges: bytes\r\nContent-Length: {Contenido.Length}{ObtenerEncabezados}\r\n";
            }
        }

        private string ObtenerEncabezados => 
            string.Join(
                        string.Empty, textoEncabezados.ToArray()
                        );

        private IEnumerable<string> textoEncabezados =>
            Encabezados?.Select(
                        r => $"\r\n{r.Nombre}: {r.Valor}"
                    ) ?? new List<string>();
    }
}
