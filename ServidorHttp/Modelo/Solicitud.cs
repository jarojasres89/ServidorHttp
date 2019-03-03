using ServidorHttp.Enums;

namespace ServidorHttp.Modelo
{
    public class Solicitud
    {
        public Solicitud(string semilla, VerbosHttp verbo, string url, byte[] contenido, string tipoContenido, Encabezado[] encabezados)
        {
            Semilla = semilla;
            Verbo = verbo;
            URL = url;
            Contenido = contenido;
            TipoContenido = tipoContenido;
            Encabezados = encabezados;
        }
        public VerbosHttp Verbo { get; }
        public string URL { get; }
        public byte[] Contenido { get; }
        public string TipoContenido { get; }
        public Encabezado[] Encabezados { get; }
        public string Semilla { get; }
    }
}
