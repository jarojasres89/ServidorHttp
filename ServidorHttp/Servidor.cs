using ServidorHttp.Modelo;
using ServidorHttp.Servicios.Acciones;
using ServidorHttp.Servicios.CreadorRespuesta;
using ServidorHttp.Servicios.EscritorLog;
using ServidorHttp.Servicios.Interprete;
using System;
using System.IO;
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
            var reader = new StreamReader(cliente.GetStream());
            var msg = string.Empty;

            while (reader.Peek() != -1)
            {
                msg += reader.ReadLine() + "\n";
            }
            
            Log("Request: \n" + msg);

            Solicitud = Interprete.InterpretarSolicitud(msg);

            var respuesta = ListaAcciones.EjecutarAcciones(Solicitud) ?? CreadorRespuesta.CrearRespuesta(Solicitud);
            
            Interprete.EscribirRespuesta(respuesta, cliente);
        }

        private void Log(string mensaje)
        {
            EscritorLog.RegistrarLog(mensaje);
        }
    }
}
