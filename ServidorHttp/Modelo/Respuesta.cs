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

    }
}
