using ServidorHttp.Modelo;
using System.Collections.Generic;

namespace ServidorHttp.Servicios.Acciones
{
    public class ListaAcciones
    {
        public List<IAccion> Acciones { get; set; }
        public void Agregar(IAccion Accion) { }
        public void Borrar(int index) { }
        public Respuesta EjecutarAcciones(Solicitud solicitud)
        {
            return null;
        }
    }
}
