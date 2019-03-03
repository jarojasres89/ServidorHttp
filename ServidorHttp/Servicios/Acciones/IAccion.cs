using ServidorHttp.Modelo;
using System;

namespace ServidorHttp.Servicios.Acciones
{
    public interface IAccion
    {
        Func<Solicitud, bool> Condicion { get; set; }
        Func<Solicitud, bool> Proceso { get; set; }
    }
}
