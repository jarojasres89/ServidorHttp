using ServidorHttp.Modelo;
using System.Net.Sockets;

namespace ServidorHttp.Servicios.Interprete
{
    public interface IInterpreteHttp
    {
        Solicitud InterpretarSolicitud(object valor);
        
        void EscribirRespuesta(Respuesta respuesta, TcpClient cliente);
    }
}
