using ServidorHttp.Modelo;
using System.Net.Sockets;

namespace ServidorHttp.Servicios.Interprete
{
    public interface IInterpreteHttp
    {
        Solicitud InterpretarSolicitud(TcpClient cliente);
        
        void EscribirRespuesta(Respuesta respuesta, TcpClient cliente);

        string ObtenerEncabezados(Encabezado[] encabezados);
    }
}
