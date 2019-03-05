using ServidorHttp.Modelo;
using ServidorHttp.Servicios.Acciones;
using ServidorHttp.Servicios.CreadorRespuesta;
using ServidorHttp.Servicios.EscritorLog;
using ServidorHttp.Servicios.Interprete;
using System.IO;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServidorHttp
{
    public class Servidor
    {

        private TcpListener _listener;

        public Servidor()
        {
            Interprete = new InterpreteHttp11();
            CreadorRespuesta = new CreadorRespuesta200();
            EscritorLog = new EscritorLogFile();
            ListaAcciones = new ListaAcciones();
        }

        public Enums.EstadosServidor Estado { get; private set; } = Enums.EstadosServidor.Detenido;
        public Solicitud Solicitud { get; set; }
        public Respuesta Respuesta { get; set; }
        public IEscritorLog EscritorLog { get; set; }

        public ListaAcciones ListaAcciones { get; set; }
        public IInterpreteHttp Interprete { get; set; }
        public ICreadorRespuesta CreadorRespuesta { get; set; }

        public void Iniciar(int puerto = 80)
        {
            var ip = IPAddress.Any;
            _listener = new TcpListener(ip, puerto);

            Log($"Iniciar servidor con IP: {ip} Puerto: {puerto}");

            var threadServidor = new Thread(new ThreadStart(IniciarServidor));
            threadServidor.Start();
        }

        private void IniciarServidor()
        {
            Estado = Enums.EstadosServidor.Iniciado;
            _listener.Start();

            while (Estado == Enums.EstadosServidor.Iniciado)
            {
                Log("Esperando por conexión...");

                var cliente = _listener.AcceptTcpClient();

                Log("Cliente conectado");

                RecibirPeticion(cliente);

                cliente.Close();
            }
            Estado = Enums.EstadosServidor.Detenido;
            _listener.Stop();
        }

        public void Detener()
        {
            Estado = Enums.EstadosServidor.Detenido;
        }

        private void RecibirPeticion(TcpClient cliente)
        {
            Solicitud = Interprete.InterpretarSolicitud(cliente);

            Log("\r\nRequest: " + Solicitud.Semilla);

            Log($"\r\nSolicitud tipada: Verbo:{Solicitud.Verbo} - Url:{Solicitud.URL} - Tipo Contenido:{Solicitud.TipoContenido} - Encabezados:{Interprete.ObtenerEncabezados(Solicitud.Encabezados)} - Contenido:{Solicitud.Contenido}");

            Respuesta = ListaAcciones.EjecutarAcciones(Solicitud) ?? CreadorRespuesta.CrearRespuesta(Solicitud);

            Log("\r\nRespuesta: " + Interprete.MensajeRespuesta(Respuesta));

            Interprete.EscribirRespuesta(Respuesta, cliente);
        }

        private void Log(string mensaje)
        {
            EscritorLog.RegistrarLog(mensaje);
        }
    }
}
