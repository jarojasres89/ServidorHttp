using ServidorHttp.Modelo;

namespace ServidorHttp.Servicios.CreadorRespuesta
{
    public interface ICreadorRespuesta
    {
        Respuesta CrearRespuesta(Solicitud solicitud);
    }
}
